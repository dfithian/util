using System;
using Fithian.Logging;
using Newtonsoft.Json;

namespace Test
{
    public class TestConfig
    {
        public static String filename = "test.cfg";
        [JsonPropertyAttribute("stringField")]
        public String stringField;
        [JsonPropertyAttribute("intField")]
        public int intField;
        [JsonPropertyAttribute("boolField")]
        public bool boolField;
        [JsonPropertyAttribute("arrayField")]
        public String[] arrayField;

        public void Debug() {
            Logger logger = LoggerFactory.GetLogger(typeof(TestConfig));
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
    }
}
