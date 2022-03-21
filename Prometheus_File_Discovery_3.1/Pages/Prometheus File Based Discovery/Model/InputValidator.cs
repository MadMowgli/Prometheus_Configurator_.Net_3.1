using System.Collections.Generic;
using System.IO;
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

        public Dictionary<string, bool> validateNewTarget(Dictionary<string, object> inputDict, dynamic dynamicConfig)
        {
            Dictionary<string, bool> returnDict = new Dictionary<string, bool>();
            
            foreach (var entry in inputDict)
            {
                if (entry.Key == "Job Name")
                {
                    // Check for format
                    string compare = (string) entry.Value;
                    bool formatIsValid = Regex.IsMatch(compare, @"[a-zA-Z\d]{1,80}");

                    // Check for double entry
                    bool nameNoDuplicate = true;
                    for (int i = 0; i < dynamicConfig["scrape_configs"].Count; i++)
                    {
                        if (dynamicConfig["scrape_configs"][i]["job_name"] == compare)
                        {
                            nameNoDuplicate = false;
                        }
                    }

                    List<bool> validationList = new List<bool>();
                    validationList.Add(formatIsValid);
                    validationList.Add(nameNoDuplicate);
                    foreach (bool result in validationList)
                    {
                        if (!result)
                        {
                            returnDict.Add(entry.Key, false);
                        }
                    }
                }

                if (entry.Key == "Targets")
                {
                    List<string> compare = (List<string>) entry.Value;
                    bool isValid = true;
                    foreach (string target in compare)
                    {
                        if (!Regex.IsMatch(target, @"(?:[0-9]{1,3}\.){3}[0-9]{1,3}\:[0-9]{2,5}"))
                        {
                            isValid = false;
                        }
                    }
                    returnDict.Add(entry.Key, isValid);
                }
                
                if (entry.Key == "Labels")
                {
                    List<ConfigurationComponents.Label> compare = (List<ConfigurationComponents.Label>) entry.Value;
                    bool isValid = true;
                    foreach (ConfigurationComponents.Label label in compare)
                    {
                        string key = label.key;
                        string value = label.value;
                        if (!Regex.IsMatch(key, @"") || !Regex.IsMatch(value, @""))
                        {
                            isValid = false;
                        }
                    }
                    returnDict.Add(entry.Key, isValid);
                }
                
                if (entry.Key == "Scrape Interval")
                {
                    string compare = (string) entry.Value;
                    bool isValid = Regex.IsMatch(compare, @"^\d+s$");
                    returnDict.Add(entry.Key, isValid);
                }
                
                if (entry.Key == "Scrape Timeout")
                {
                    string compare = (string) entry.Value;
                    bool isValid = Regex.IsMatch(compare, @"^\d+s$");
                    returnDict.Add(entry.Key, isValid);
                }
                
                if (entry.Key == "Metrics Path")
                {
                    string compare = (string) entry.Value;
                    bool isValid = Regex.IsMatch(compare, @"^/[a-z]+$");
                    returnDict.Add(entry.Key, isValid);
                }
                
                if (entry.Key == "Scheme")
                {
                    string compare = (string) entry.Value;
                    bool isValid = compare == "http" || compare == "https";
                    returnDict.Add(entry.Key, isValid);
                }
            }
            
            
            return returnDict;
        }

    }
}

