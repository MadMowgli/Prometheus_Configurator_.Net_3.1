using System.Collections.Generic;

namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Model
{
    public class ApiConfiguration
    {
        // Fields
        private Dictionary<string, string> configs;

        // Constructor
        public ApiConfiguration()
        {
            this.configs = new Dictionary<string, string>();
        }

        // Methods
        public Dictionary<string, string> getConfigs()
        {
            return this.configs;
        }

    }
}

