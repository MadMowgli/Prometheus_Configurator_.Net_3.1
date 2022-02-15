// This class represents a Prometheus Job. Each Prometheus job is an object that gets listed in the scrape_configs array
// of the Prometheus configuration file.

using System.Collections.Generic;

namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Model
{
    public class PrometheusJob
    {
        // Fields

        // Constructor with no arguments
        public PrometheusJob()
        {
        }

        // Constructor taking all parameters
        public PrometheusJob(string job_name, string scrape_interval, string scrape_timeout,
            string metrics_path, string scheme, List<string> targets, List<ConfigurationComponents.Label> labels)
        {
            JobName = job_name;
            Scrape_Interval = scrape_interval;
            Scrape_Timeout = scrape_timeout;
            Metrics_path = metrics_path;
            Scheme = scheme;
            Targets = targets;
            Labels = labels;
        }

        // Properties
        public string JobName { get; set; }

        public string Scrape_Interval { get; set; }

        public string Scrape_Timeout { get; set; }

        public string Metrics_path { get; set; }

        public string Scheme { get; set; }

        public bool Honor_Labels { get; set; } = false;

        public List<string> Targets { get; set; }

        public List<ConfigurationComponents.Label> Labels { get; set; }

        public Dictionary<string, List<string>> File_Sd_Configs { get; set; }

        public List<ConfigurationComponents.Static_Configs> Static_Configs { get; set; } =
            new List<ConfigurationComponents.Static_Configs>();

        // Custom Methods
        public void addLabel(string key, string value)
        {
            if (Labels == null) Labels = new List<ConfigurationComponents.Label>();
            Labels.Add(new ConfigurationComponents.Label(key, value));
        }

        public void removeLabel(string key)
        {
            // this.Labels.Remove(key);
        }

        public void addTarget(string target)
        {
            if (Targets == null) Targets = new List<string>();
            Targets.Add(target);
        }

        public void removeTarget(string target)
        {
            Targets.Remove(target);
        }

        public void addFileSdConfig(string key, string value)
        {
            var configs = new List<string>();
            configs.Add(value);
            File_Sd_Configs.Add(key, configs);
        }

        public ConfigurationComponents.Scrape_Configs toScrapeConfigObject()
        {
            var scrapeConfigObject = new ConfigurationComponents.Scrape_Configs();

            if (JobName != null) scrapeConfigObject.job_name = JobName;
            if (Scrape_Interval != null) scrapeConfigObject.scrape_interval = Scrape_Interval;
            if (Scrape_Timeout != null) scrapeConfigObject.scrape_timeout = Scrape_Timeout;
            if (Metrics_path != null && Metrics_path != "") scrapeConfigObject.metrics_path = Metrics_path;
            if (Scheme != null) scrapeConfigObject.scheme = Scheme;
            if (Static_Configs != null) scrapeConfigObject.static_configs = Static_Configs;

            return scrapeConfigObject;
        }
    }
}