using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prometheus_File_Discovery_.NET_Core_3._1.Pages.Prometheus_File_Based_Discovery.Model
{
    public class InputValidator
    {
        // Fields
        private static InputValidator self;
        private readonly Regex sWhiteSpace;
    
        // Singleton
        private InputValidator()
        {
            this.sWhiteSpace = new Regex(@"\s+|\\\""");
        }
        public static InputValidator getInputValidator()
        {
            if (self == null)
            {
                self = new InputValidator();
                return self;
            }

            return self;
        }
    
        // Methods
        public string validateJson(string jsonString)
        {
            string returnString = jsonString;

            // Replace literal carriage returns and newlines
            returnString = returnString.Replace(@"\r", "").Replace(@"\n", "");
        
            // Check for empty labels
            JArray jArray = JsonConvert.DeserializeObject<JArray>(returnString);
            foreach (JObject jObject in jArray)
            {
                if (jObject.ContainsKey("labels"))
                {
                    if (!jObject["labels"].HasValues)
                    {
                        jObject.Remove("labels");
                    }
                }
            }

            returnString = JsonConvert.SerializeObject(jArray, Formatting.Indented);
            return returnString;
        }

        public string prepareApiJson(string jsonString)
        {
            string returnString = this.sWhiteSpace.Replace(jsonString, "");
            returnString = returnString
                .Replace(@"\r\n", "")
                .Replace(@"\\r\\n", "\n");
            return returnString;
        }
    }
}

