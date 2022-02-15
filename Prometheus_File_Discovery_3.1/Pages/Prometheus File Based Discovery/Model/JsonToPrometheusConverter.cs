using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Model
{

    public static class JsonToPrometheusConverter
    {
        // Fields


        // Constructor


        // Methods
        public static PrometheusConfiguration convertJsonToDotNet(dynamic dynamicConfig,
            PrometheusConfiguration prometheusConfiguration)
        {
            /* NAMING CONVENTION
             * - configProperty = elements of the prometheus configuration (global, rule_files, scrape_configs, ...)
             */

            // Loop over each attribute to parse it
            foreach (var configProperty in dynamicConfig.Properties())
            {
                string configPropertyName = configProperty.Name;
                switch (configPropertyName)
                {
                    case "global":
                        // Create new configComponent
                        var global = new ConfigurationComponents.Global();
                        Console.WriteLine("Parsing global component...");

                        // Loop over object properties
                        foreach (JObject globalComponentValues in configProperty.Values<object>())
                            // Check if property has properties itself
                            if (globalComponentValues.Properties().Any())
                                foreach (var globalComponentProperty in globalComponentValues.Properties())
                                {
                                    // Check for each subtype
                                    var propName = globalComponentProperty.Name;
                                    Console.WriteLine("Parsing " + propName + " of global component...");

                                    if (propName == "scrape_interval")
                                    {
                                        global.scrape_interval = globalComponentProperty.Value.ToString();
                                        Console.WriteLine("Scrape Interval: " + globalComponentProperty.Value);
                                    }

                                    if (propName == "evaluation_interval")
                                    {
                                        global.evaluation_interval = globalComponentProperty.Value.ToString();
                                        Console.WriteLine("Evaluation Interval: " + globalComponentProperty.Value);
                                    }
                                    // if(name.Equals("external_labels")) { global.evaluation_interval = prop.Value.ToString(); }
                                }

                        // Assign new configComponent
                        prometheusConfiguration.global = global;
                        break;


                    case "scrape_configs":
                        // Create new configComponent
                        // Each scrape_config element should represents a prometheus job, according to the docs.
                        Console.WriteLine("Parsing scrape_configs component...");

                        // Loop over object properties
                        // The configProperty is actually a list (JArray) containing jobObjects
                        foreach (JArray jobArray in configProperty.Values<object>())
                        {
                            Console.WriteLine("Found Prometheus Jobs: " + jobArray.Count);
                            if (jobArray.Values().Any())
                            {
                                var count = 0;
                                // foreach (JProperty jobProperty in jobArray.Values())
                                foreach (JObject job in jobArray.Children())
                                {
                                    var prometheusJob = new PrometheusJob();
                                    count++;
                                    // TODO: Continue here, 10.02.22
                                    // Each object is a new PrometheusJob, parse it's data
                                    foreach (var jobProperty in job.Properties())
                                    {
                                        var propName = jobProperty.Name;
                                        var propValue = jobProperty.Value.ToString();

                                        if (propName.Equals("job_name"))
                                        {
                                            prometheusJob.JobName = propValue;
                                            Console.WriteLine("Job Name: " + propValue);
                                        }

                                        if (propName.Equals("scrape_interval"))
                                        {
                                            prometheusJob.Scrape_Interval = propValue;
                                            Console.WriteLine("Scrape Interval: " + propValue);
                                        }

                                        if (propName.Equals("scrape_timeout"))
                                        {
                                            prometheusJob.Scrape_Timeout = propValue;
                                            Console.WriteLine("Scrape Timeout: " + propValue);
                                        }

                                        if (propName.Equals("honor_labels"))
                                        {
                                            prometheusJob.Honor_Labels = Convert.ToBoolean(propValue);
                                            Console.WriteLine("Honor Labels: " + propValue);
                                        }

                                        if (propName.Equals("scheme"))
                                        {
                                            prometheusJob.Scheme = propValue;
                                            Console.WriteLine("Scheme: " + propValue);
                                        }

                                        if (propName.Equals("static_configs"))
                                        {
                                            Console.WriteLine("Reading static_configs...");
                                            // The static_configs element contains an array of static_configs
                                            foreach (JArray staticConfigArray in jobProperty.Children())
                                            {
                                                // Each element inside this array is a static_configs object
                                                foreach (JObject staticConfigJObject in staticConfigArray.Children())
                                                {
                                                    var static_Configs = new ConfigurationComponents.Static_Configs();
                                                    Console.WriteLine("staticConfigObject: " + staticConfigArray);

                                                    //Loop over each property
                                                    foreach (var staticConfigProperty in
                                                             staticConfigJObject.Properties())
                                                    {
                                                        var staticConfigPropertyName = staticConfigProperty.Name;

                                                        // Target list
                                                        if (staticConfigPropertyName == "targets")
                                                        {
                                                            Console.Write("Targets: ");
                                                            foreach (JValue scrapeTarget in
                                                                     staticConfigProperty.Values())
                                                            {
                                                                static_Configs.targets.Add(scrapeTarget.ToString());
                                                                Console.WriteLine(scrapeTarget.ToString());
                                                            }
                                                        }

                                                        // Labels
                                                        if (staticConfigPropertyName == "labels")
                                                        {
                                                            Console.Write("Labels: ");
                                                            foreach (dynamic customLabel in
                                                                     staticConfigProperty.Values())
                                                            {
                                                                string key = customLabel.Name.ToString();
                                                                string value = customLabel.Value.ToString();
                                                                Console.WriteLine(key + ":" + value);
                                                                static_Configs.labels.Add(key, value);
                                                            }
                                                        }
                                                    }

                                                    prometheusJob.Static_Configs.Add(static_Configs);
                                                }
                                            }
                                        }
                                    }

                                    // Assign new configComponent
                                    ConfigurationComponents.Scrape_Configs
                                        scrapeConfig = prometheusJob.toScrapeConfigObject();
                                    prometheusConfiguration.scrape_configs
                                        .Add(scrapeConfig); // This produced a nullPointerException once
                                }
                            }
                        }


                        break;


                    case "alerting":
                        // Create new configComponent
                        var alerting = new ConfigurationComponents.Alerting();
                        Console.WriteLine("Parsing alerting component...");

                        // Check if not empty
                        if (configProperty.HasValues)
                        {
                            foreach (JObject alertManagersObject in configProperty.Values<object>())
                            {
                                foreach (JProperty alertManagerObject in alertManagersObject.Properties())
                                {

                                    // Each obect inside the array is a new alertManager
                                    foreach (JObject alertManager in alertManagerObject.Values())
                                    {
                                        ConfigurationComponents.Alertmanager prometheusAlertManager =
                                            new ConfigurationComponents
                                                .Alertmanager();

                                        foreach (JProperty alertManagerProperty in alertManager.Properties())
                                        {
                                            var propName = alertManagerProperty.Name;
                                            var propValue = alertManagerProperty.Value.ToString();

                                            if (propName.Equals("scheme"))
                                            {
                                                prometheusAlertManager.scheme = propValue;
                                                Console.WriteLine("Alertmanager scheme: " + propValue);
                                            }

                                            if (propName.Equals("static_configs"))
                                            {
                                                Console.WriteLine("Reading static_configs for alertManager...");
                                                // The static_configs element contains an array of static_configs
                                                foreach (JArray staticConfigArray in alertManagerProperty.Children())
                                                {
                                                    // Each element inside this array is a static_configs object
                                                    foreach (JObject staticConfigJObject in
                                                             staticConfigArray.Children())
                                                    {
                                                        var static_Configs =
                                                            new ConfigurationComponents.Static_Configs();
                                                        Console.WriteLine("staticConfigObject: " + staticConfigArray);

                                                        //Loop over each property
                                                        foreach (var staticConfigProperty in
                                                                 staticConfigJObject.Properties())
                                                        {
                                                            var staticConfigPropertyName = staticConfigProperty.Name;

                                                            // Target list
                                                            if (staticConfigPropertyName == "targets")
                                                            {
                                                                Console.Write("Targets: ");
                                                                foreach (JValue scrapeTarget in
                                                                         staticConfigProperty.Values())
                                                                {
                                                                    static_Configs.targets.Add(scrapeTarget.ToString());
                                                                    Console.WriteLine(scrapeTarget.ToString());
                                                                }
                                                            }

                                                            // Labels
                                                            if (staticConfigPropertyName == "labels")
                                                            {
                                                                Console.Write("Labels: ");
                                                                foreach (dynamic customLabel in
                                                                         staticConfigProperty.Values())
                                                                {
                                                                    string key = customLabel.Name.ToString();
                                                                    string value = customLabel.Value.ToString();
                                                                    Console.WriteLine(key + ":" + value);
                                                                    static_Configs.labels.Add(key, value);
                                                                }
                                                            }
                                                        }

                                                        prometheusAlertManager.static_configs.Add(static_Configs);
                                                    }
                                                }
                                            }
                                        }

                                        alerting.alertmanagers.Add(prometheusAlertManager);
                                    }
                                }
                            }
                        }

                        // Assign new configComponent
                        prometheusConfiguration.alerting = alerting;
                        break;


                    case "rule_files":
                        // Create new configComponent
                        var ruleFiles = new List<string>();
                        Console.WriteLine("Parsing rule_files component...");

                        // Looop over property values
                        foreach (JArray jArr in configProperty.Values<dynamic>())
                        foreach (var jArrVal in jArr.Value<dynamic>())
                        {
                            Console.WriteLine("Rule Files: " + jArrVal.ToString());
                            ruleFiles.Add(jArrVal.ToString());
                        }

                        // Assign new configComponent
                        prometheusConfiguration.rule_files = ruleFiles;
                        break;
                }
            }

            return prometheusConfiguration;
        }
    }
}