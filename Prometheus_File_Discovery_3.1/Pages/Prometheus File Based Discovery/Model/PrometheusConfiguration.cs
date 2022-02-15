using System.Collections.Generic;
using Newtonsoft.Json;

namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Model
{
    public class PrometheusConfiguration
    {
        // Remote read & Write are commented out since we don't need this in our current setup.
        // public ConfigurationComponents.Remote_Write remote_write { get; set; }
        // public ConfigurationComponents.Remote_Read remote_read { get; set; }


        // Constructor
        public PrometheusConfiguration()
        {
            Version = 0.0;
            global = new ConfigurationComponents.Global();
            rule_files = new List<string>();
            scrape_configs = new List<ConfigurationComponents.Scrape_Configs>();
            alerting = new ConfigurationComponents.Alerting();

            // remote_write = new ConfigurationComponents.Remote_Write();
            // remote_read = new ConfigurationComponents.Remote_Read();
        }

        // Properties
        public double Version { get; set; }
        public ConfigurationComponents.Global global { get; set; }
        public List<string> rule_files { get; set; }
        public List<ConfigurationComponents.Scrape_Configs> scrape_configs { get; set; }
        public ConfigurationComponents.Alerting alerting { get; set; }

        // ToString method
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}