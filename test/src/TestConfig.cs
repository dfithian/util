using System;
using System.ComponentModel;
using Fithian.Logging;
using Fithian.Config;
using Newtonsoft.Json;

namespace Test
{
    public class TestConfig
    {
        public static string filename = "test.cfg";
        [JsonPropertyAttribute("stringField")]
        public string stringField {get; set;}
        [JsonPropertyAttribute("intField")]
        public int intField {get; set;}
        [JsonPropertyAttribute("boolField")]
        public bool boolField {get; set;}
        [JsonPropertyAttribute("arrayField")]
        public string[] arrayField {get; set;}

        public void Debug() {
            Logger logger = LoggerFactory.GetLogger<TestConfig>();
            logger.Debug("stringField: " + stringField);
            logger.Debug("intField: " + intField.ToString());
            logger.Debug("boolField: " + boolField.ToString());
            logger.Debug("arrayField...");
            if (arrayField != null) {
                foreach (String s in arrayField) {
                    logger.Debug("array string: " + s);
                }
            }
        }

        //Add your defaults here
        public TestConfig() {
            this.stringField = "one";
            this.intField = 1;
            this.boolField = true;
            this.arrayField = new string[] {"one", "two"};
        }

        //Add a static method to your class to call into the config factory
        public static TestConfig FromFile(string filename, Logger logger) {
            return ConfigFactory<TestConfig>.FromFile(filename, logger);
        }
    }
}
