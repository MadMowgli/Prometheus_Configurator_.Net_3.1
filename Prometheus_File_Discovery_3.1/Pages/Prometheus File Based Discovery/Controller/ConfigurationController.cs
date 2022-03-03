using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Model;

namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Controller
{
    public class ConfigurationController : Microsoft.AspNetCore.Mvc.Controller
    {
        // Fields
        InputValidator inputValidator = InputValidator.getInputValidator();

        [HttpGet]
        public string Info()
        {
            return "Info";
        }
        
        // GET
        [HttpGet]
        [Route("getConfig")]
        public async Task<JsonResult> getConfig()
        {
            try
            {
                // Add json header
                HttpContext.Response.Headers.Add("content-type", "application/json");
            
                // Create new object to stuff data
                ApiConfiguration apiConfiguration = new ApiConfiguration();
            
                // Add main prometheus config
                string promConfig = await System.IO.File.ReadAllTextAsync("Data/prometheus.yml");
                promConfig = promConfig.Replace(@"\r\n", "");
                apiConfiguration.getConfigs().Add("prometheus.yml", promConfig);

                // Look for all the JSON files and append them to an array
                foreach (var fileEntry in Directory.GetFiles("Data", "*.json"))
                {
                    string fileName = Path.GetFileName(fileEntry);
                    apiConfiguration.getConfigs().Add(fileName, await System.IO.File.ReadAllTextAsync(fileEntry));
                }

            
                // Holy crap.. Blazor wasn't able to return a JObject because it double-serialized it.
                // https://stackoverflow.com/questions/49330187/jsonconverter-serialize-failing-returning-a-string-rather-than-json-object ...?!
                return Json(apiConfiguration.getConfigs());
            }
            catch (Exception e)
            {
                return Json("None");
            }
            

            
        }

        [HttpGet]
        [Route("getChecksum")]
        public async Task<JsonResult> getChecksum()
        {
            try
            {
                // Add json header
                HttpContext.Response.Headers.Add("content-type", "application/json");
            
                using (var md5 = MD5.Create())
                {
                    using (var stream = System.IO.File.OpenRead("Data/prometheus.yml"))
                    {
                        Dictionary<string, string> data = new Dictionary<string, string>();
                        data.Add("Checksum", BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant());
                        return Json(data);
                    }
                }
            }
            catch (Exception e)
            {
                return Json("None");
            }
            
        }
    }
}

