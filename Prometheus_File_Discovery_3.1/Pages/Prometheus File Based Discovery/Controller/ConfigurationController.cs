using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Controller
{
    public class ConfigurationController : Microsoft.AspNetCore.Mvc.Controller
    {

        [HttpGet]
        public string Info()
        {
            return "Info";
        }
        
        // GET
        [HttpGet]
        [Route("getConfig")]
        public async Task<ActionResult> getConfig()
        {
            // Read prometheus config
            string mainConfig = await System.IO.File.ReadAllTextAsync("Data/prometheus.yml");
        
            return Ok(mainConfig);
        }
    }
}

