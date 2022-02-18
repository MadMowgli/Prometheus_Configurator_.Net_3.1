using System.Collections.Generic;
using Newtonsoft.Json;

namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Model
{
/*
 * This class serves as a wrapper for all the small classes used as configuration components.
 * Thereby, the wrapper class should not be instantiated, which is why it is set as static.
 * For the full documentation of each component, refer to the Prometheus Documentation:
 * https://prometheus.io/docs/prometheus/latest/configuration/configuration/
 */

    public static class ConfigurationComponents
    {
        /*********************************************************************************
         * Default Prometheus Configuration Components
         */
        public class Alerting
        {
            // Properties
            public List<Alertmanager> alertmanagers { get; set; } = new List<Alertmanager>();
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Alertmanager
        {
            // Fields
            private string _scheme;

            // Properties
            public string scheme { get; set; }
            public List<Static_Configs> static_configs { get; set; } = new List<Static_Configs>();
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Authorization
        {
            // Constructors
            public Authorization(string credentials)
            {
                this.credentials = credentials;
            }

            public Authorization(string credentials_file, bool hasCredentialFile)
            {
                this.credentials_file = credentials_file;
            }

            // Properties
            public string credentials_file { get; set; }
            public string credentials { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Basic_Auth
        {
            // Constructors
            public Basic_Auth(string username, string password)
            {
                username = username;
                password = password;
            }

            public Basic_Auth(string username, string passwordFile, bool hasPasswordFile)
            {
                this.username = username;
                password_file = passwordFile;
            }

            // Properties
            public string username { get; set; }
            public string password { get; set; }
            public string password_file { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Dns_Sd_Configs
        {
            public string refresh_interval { get; set; }
            public List<string> names { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Filter
        {
            public string name { get; set; }
            public List<string> values { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class File_Sd_Configs
        {
            public File_Sd_Configs(List<string> files, string refresh_interval)
            {
                this.files = files;
                this.refresh_interval = refresh_interval;
            }

            public List<string> files { get; set; }
            public string refresh_interval { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Global
        {
            // Properties
            public string? scrape_interval { get; set; }
            public string scrape_timeout { get; set; } = "5s";

            public string? evaluation_interval { get; set; }
            // public List<string>? external_labels { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Headers
        {
            public string name { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Http_Sd_Configs
        {
            public string url { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Label
        {
            // Constructor
            public Label(string key, string value)
            {
                this.key = key;
                this.value = value;
            }

            // Properties
            public string key { get; set; }
            public string value { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Metric_Relabel_Configs
        {
            public List<string> source_labels { get; set; }
            public string regex { get; set; }
            public string action { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Node_Meta
        {
            public string rack { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Namespaces
        {
            public List<string> names { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Oauth2
        {
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string token_url { get; set; }
            public Tls_Config tls_config { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Remote_Read
        {
            public string url { get; set; }
            public bool read_recent { get; set; }
            public string name { get; set; }
            public Required_Matchers required_matchers { get; set; }
            public Tls_Config tls_config { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Remote_Write
        {
            public string url { get; set; }
            public string name { get; set; }
            public List<Write_Relabel_Configs> write_relabel_configs { get; set; }
            public Oauth2 oauth2 { get; set; }
            public Tls_Config tls_config { get; set; }
            public Headers headers { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Required_Matchers
        {
            public string job { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Relabel_Configs
        {
            public List<string> source_labels { get; set; }
            public string regex { get; set; }
            public string target_label { get; set; }
            public string replacement { get; set; }
            public string action { get; set; }
            public int modulus { get; set; }
            public string separator { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Static_Configs
        {
            // Properties
            public List<string> targets { get; set; } = new List<string>();
            public Dictionary<string, string> labels { get; set; } = new Dictionary<string, string>();

            // Constructor
            public Static_Configs()
            {
            }

            public Static_Configs(List<string> targets, Dictionary<string, string> labels)
            {
                this.targets = targets;
                this.labels = labels;
            }
        }

        public class Labels
        {
            // Constructor
            public Labels()
            {
                props = new Dictionary<string, string>();
            }

            // Properties
            public IDictionary<string, string> props { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Scrape_Configs_Custom
        {
            // Constructor
            public Scrape_Configs_Custom()
            {
                prometheus_jobs = new List<PrometheusJob>();
            }

            // Properties
            public List<PrometheusJob> prometheus_jobs { get; set; } = new List<PrometheusJob>();

            // Wrapper method to add prometheus job fast
            public void addPrometheusJob(PrometheusJob job)
            {
                prometheus_jobs.Add(job);
            }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Scrape_Configs
        {
            // Constructor
            public Scrape_Configs()
            {
                job_name = "";
                honor_labels = false; // default value: false
                honor_timestamps = true; // default value: true
                scrape_interval = "15s";
                scrape_timeout = "5s";
                scheme = "http"; // default value: http
                // file_sd_configs = new List<File_Sd_Configs>();
                static_configs = new List<Static_Configs>();
                metrics_path = "/metrics"; // default value: /metrics
            }

            // Fields
            public string job_name { get; set; }
            public bool honor_labels { get; set; }
            public bool honor_timestamps { get; set; }
            public string scrape_interval { get; set; }
            public string scrape_timeout { get; set; }
            public string metrics_path { get; set; }
            public string scheme { get; set; }
            public List<File_Sd_Configs> file_sd_configs { get; set; }
            public List<Static_Configs> static_configs { get; set; }


            // Potential additions
            // public Authorization? authorization { get; set; }
            // public Basic_Auth? basic_auth { get; set; }
            // public string? body_size_limit { get; set; }
            // public Tls_Config? tls_config { get; set; }
            // public int? sample_limit { get; set; }

            // public List<Relabel_Configs>? relabel_configs { get; set; }
            // public List<Dns_Sd_Configs>? dns_sd_configs { get; set; }
            // public List<Metric_Relabel_Configs>? metric_relabel_configs { get; set; }
            // public List<Consul_Sd_Configs>? consul_sd_configs { get; set; }
            // public List<Kubernetes_Sd_Configs>? kubernetes_sd_configs { get; set; }
            // public List<Kuma_Sd_Configs>? kuma_sd_configs { get; set; }
            // public List<Marathon_Sd_Configs>? marathon_sd_configs { get; set; }
            // public List<Ec2_Sd_Configs>? ec2_sd_configs { get; set; }
            // public List<Lightsail_Sd_Configs>? lightsail_sd_configs { get; set; }
            // public List<Azure_Sd_Configs>? azure_sd_configs { get; set; }
            // public List<Nerve_Sd_Configs>? nerve_sd_configs { get; set; }
            // public List<Http_Sd_Configs>? http_sd_configs { get; set; }
            // public List<Triton_Sd_Configs>? triton_sd_configs { get; set; }
            // public List<Digitalocean_Sd_Configs>? digitalocean_sd_configs { get; set; }
            // public List<Docker_Sd_Configs>? docker_sd_configs { get; set; }
            // public List<Dockerswarm_Sd_Configs>? dockerswarm_sd_configs { get; set; }
            // public List<Openstack_Sd_Configs>? openstack_sd_configs { get; set; }
            // public List<Puppetdb_Sd_Configs>? puppetdb_sd_configs { get; set; }
            // public List<Hetzner_Sd_Configs>? hetzner_sd_configs { get; set; }
            // public List<Eureka_Sd_Configs>? eureka_sd_configs { get; set; }
            // public List<Scaleway_Sd_Configs>? scaleway_sd_configs { get; set; }
            // public List<Linode_Sd_Configs>? linode_sd_configs { get; set; }
            // public List<Uyuni_Sd_Configs>? uyuni_sd_configs { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Tls_Config
        {
            public string ca_file { get; set; }
            public string cert_file { get; set; }
            public string key_file { get; set; }
            public bool insecure_skip_verify { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Write_Relabel_Configs
        {
            public List<string> source_labels { get; set; }
            public string regex { get; set; }
            public string action { get; set; }
        }


        /*********************************************************************************
         * Special Configuration Components
         */
        public class Consul_Sd_Configs
        {
            public string server { get; set; }
            public string token { get; set; }
            public string[] services { get; set; }
            public string[] tags { get; set; }
            public Node_Meta node_meta { get; set; }
            public bool allow_stale { get; set; }
            public string scheme { get; set; }
            public Tls_Config tls_config { get; set; }
        }

        public class Kubernetes_Sd_Configs
        {
            public string role { get; set; }
            public string api_server { get; set; }
            public Tls_Config tls_config { get; set; }
            public Basic_Auth basic_auth { get; set; }
            public Namespaces namespaces { get; set; }
        }

        public class Kuma_Sd_Configs
        {
            public string server { get; set; }
        }

        public class Marathon_Sd_Configs
        {
            public string[] servers { get; set; }
            public string auth_token { get; set; }
            public Tls_Config tls_config { get; set; }
        }

        public class Ec2_Sd_Configs
        {
            public string region { get; set; }
            public string access_key { get; set; }
            public string secret_key { get; set; }
            public string profile { get; set; }
            public Filter[] filters { get; set; }
        }

        public class Lightsail_Sd_Configs
        {
            public string region { get; set; }
            public string access_key { get; set; }
            public string secret_key { get; set; }
            public string profile { get; set; }
        }

        public class Azure_Sd_Configs
        {
            public string environment { get; set; }
            public string authentication_method { get; set; }
            public string subscription_id { get; set; }
            public string tenant_id { get; set; }
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public int port { get; set; }
        }

        public class Nerve_Sd_Configs
        {
            public string[] servers { get; set; }
            public string[] paths { get; set; }
        }

        public class Triton_Sd_Configs
        {
            public string account { get; set; }
            public string dns_suffix { get; set; }
            public string endpoint { get; set; }
            public int port { get; set; }
            public string refresh_interval { get; set; }
            public int version { get; set; }
            public Tls_Config tls_config { get; set; }
        }

        public class Digitalocean_Sd_Configs
        {
            public Authorization authorization { get; set; }
        }

        public class Docker_Sd_Configs
        {
            public string host { get; set; }
        }

        public class Dockerswarm_Sd_Configs
        {
            public string host { get; set; }
            public string role { get; set; }
        }

        public class Openstack_Sd_Configs
        {
            public string role { get; set; }
            public string region { get; set; }
            public int port { get; set; }
            public string refresh_interval { get; set; }
            public Tls_Config tls_config { get; set; }
        }

        public class Puppetdb_Sd_Configs
        {
            public string url { get; set; }
            public string query { get; set; }
            public bool include_parameters { get; set; }
            public int port { get; set; }
            public string refresh_interval { get; set; }
            public Tls_Config tls_config { get; set; }
        }

        public class Hetzner_Sd_Configs
        {
            public string role { get; set; }
            public Authorization authorization { get; set; }
            public Basic_Auth basic_auth { get; set; }
        }

        public class Eureka_Sd_Configs
        {
            public string server { get; set; }
        }

        public class Scaleway_Sd_Configs
        {
            public string role { get; set; }
            public string project_id { get; set; }
            public string access_key { get; set; }
            public string secret_key { get; set; }
        }

        public class Linode_Sd_Configs
        {
            public Authorization authorization { get; set; }
        }

        public class Uyuni_Sd_Configs
        {
            public string server { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}