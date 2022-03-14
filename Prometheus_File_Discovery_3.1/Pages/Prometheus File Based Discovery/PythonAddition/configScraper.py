'''
Python:         Python 2.7.16 (v2.7.16:413a49145e, Mar  4 2019, 01:37:19) [MSC v.1500 64 bit (AMD64)]
Context:        This script is part of the Prometheus Configurator.
Author:         Joel Laeubin
Dependencies:   urllib3[secure] because of TLS warnings, check https://urllib3.readthedocs.io/en/1.26.x/user-guide.html#ssl
VarArgs:        - sys.argv[0] = script name
                - sys.argv[1] = base url of scraping point
                - sys.argv[2] = local path to the prometheus.yml configuration file (including the file name)
'''

import hashlib
import json
import logging
import os
import shutil
# imports
import sys

import requests

# --------------------------------------------------------------------------------------------- Setup
# configure logging
logging.basicConfig(filename='configScraper.log', level=logging.WARNING,
                    format='%(asctime)s|%(levelname)s|%(message)s', datefmt='%d/%m/%Y-%I:%M:%S-%p')
if len(sys.argv) < 2:
    print('You must specify the base url of your scraping point as the first script parameter.')
    logging.error('Sys.Argv-Error: No base line url provided')
    exit()

# delete logging file if size > 5MB
try:
    if os.stat(sys.path[0] + '\\' + 'configScraper.log').st_size > 5000000:
        os.remove(sys.path[0] + '\\' + 'configScraper.log')
except IOError as e:
    logging.warning('Logfile-Remove-IO-Exception: {}'.format(e.strerror))

# placeholders for global scope
local_md5 = str()
api_md5 = str()
md5_request = None
config_request = None

# set default config paths for directories & create dirs if required
local_base_path = str(sys.path[0] + '\\current')
local_config_path = str(local_base_path + '\\prometheus.yml')  # Default prometheus config path
local_backup_path = str(sys.path[0] + '\\backup')
# Create backup folder if required
try:
    if not os.path.isdir(local_base_path):
        os.mkdir(local_base_path)
    if not os.path.isdir(local_backup_path):
        os.mkdir(local_backup_path)
except IOError as e:
    logging.warning('DirSetup-IO-Exception: {}'.format(e.strerror))

# catch varargs & construct urls
base_url = str(sys.argv[1])
md5_endpoint = base_url + "/getChecksum"
config_endpoint = base_url + "/getConfig"

# checking for optional 2nd vararg containing filepath of local config
if len(sys.argv) > 2:
    local_config_path = str(sys.argv[2])
    local_base_path = str(sys.argv[2]).replace('\\prometheus.yml', '')

# --------------------------------------------------------------------------------------------- MD5 comparison
# grab md5-checksum of the main prometheus.yml file on this server & compare it with the one hosted
try:
    with open(local_config_path, 'rb') as file:
        local_md5 = hashlib.md5(file.read()).hexdigest()
except IOError as e:
    logging.warning('FileRead-IO-Exception: {}'.format(e.strerror))

# get md5-checksum from api
try:
    md5_request = requests.get(md5_endpoint, verify=False)
    if md5_request.status_code is not 200:
        logging.error('Request-error, HTTP status code: {}'.format(md5_request.status_code))
        exit()
except requests.exceptions.RequestException as e:
    logging.warning('Requests-Exception: {}'.format(e.strerror))
if md5_request is not None and md5_request.status_code is 200:
    api_md5 = json.loads(md5_request.text)
    api_md5 = api_md5['Checksum']

# --------------------------------------------------------------------------------------------- Backup & config pull
# only proceed when config changed, hence local_md5 != api_md5
if local_md5 != api_md5:

    # Iterate over each file (skips directories) in saved location
    try:
        for local_file in [f for f in os.listdir(local_base_path) if os.path.isfile(os.path.join(local_base_path, f))]:
            if '.json' in local_file or '.yml' in local_file:
                shutil.copyfile(local_base_path + '\\' + local_file, local_backup_path + '\\' + local_file)
    except IOError as e:
        logging.warning('Backup-IO-Exception: {}'.format(e.strerror))

    # make request, we may need to specify a private certificate using verify='pathToCertificate'
    try:
        config_request = requests.get(config_endpoint, verify=False)
        if config_request.status_code is not 200:
            logging.error('Request-error, HTTP status code: {}'.format(config_request.status_code))
            exit()
    except requests.exceptions.RequestException as e:
        logging.warning('Requests-Exception: {}'.format(e.strerror))

    if config_request is not None and config_request.status_code == 200:
        response = config_request.text

        # Convert to JSON object to generate files from it
        json_object = json.loads(response)
        try:
            for entry in json_object.items():
                with open(local_base_path + '\\' + entry[0], 'w') as config_file:
                    content = str(entry[1]).replace('\r', "")
                    config_file.write(content)
        except IOError as e:
            logging.warning('FileWrite-IO-Exception: {}'.format(e.strerror))
