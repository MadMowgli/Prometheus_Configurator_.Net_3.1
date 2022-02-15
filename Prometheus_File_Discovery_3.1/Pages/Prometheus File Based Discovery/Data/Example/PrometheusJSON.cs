namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Data.Example
{

    public class PrometheusJSON
    {
        public class Rootobject
        {
            public Global global { get; set; }
            public Scrape_Configs[] scrape_configs { get; set; }
            public Alerting alerting { get; set; }
            public string[] rule_files { get; set; }
        }

        public class Global
        {
            public string scrape_interval { get; set; }
        }

        public class Alerting
        {
            public Alertmanager[] alertmanagers { get; set; }
        }

        public class Alertmanager
        {
            public string scheme { get; set; }
            public string[] static_configs { get; set; }
        }

        public class Scrape_Configs
        {
            public string job_name { get; set; }
            public string scrape_interval { get; set; }
            public string scrape_timeout { get; set; }
            public Static_Configs[] static_configs { get; set; }
        }

        public class Static_Configs
        {
            public string[] targets { get; set; }
            public Labels labels { get; set; }
        }

        public class Labels
        {
            public string label_1 { get; set; }
            public string label_2 { get; set; }
            public string label_3 { get; set; }
            public string label_4 { get; set; }
            public string label_5 { get; set; }
            public string label_6 { get; set; }
            public string label_7 { get; set; }
            public string label_8 { get; set; }
            public string label_9 { get; set; }
            public string label_10 { get; set; }
            public string label_11 { get; set; }
            public string label_12 { get; set; }
        }
    }
}