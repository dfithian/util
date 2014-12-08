using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Fithian.Logging
{
    public class LoggerConfig
    {
        [JsonPropertyAttribute("defaultLogger")]
        private SingleConfig defaultConfig {get; set;}
        [JsonPropertyAttribute("loggers")]
        private SingleConfig[] loggerConfigs {get; set;}
        public class SingleConfig
        {
            [JsonPropertyAttribute("logger")]
            public string theLogger {get; set;}
            [JsonPropertyAttribute("namespace")]
            public string theNamespace {get; set;}
            [JsonPropertyAttribute("filename")]
            public string filename {get; set;}
            [JsonPropertyAttribute("pattern")]
            public string pattern {get; set;}
            private string datePattern = null;
            [JsonPropertyAttribute("level")]
            public string logLevel {get; set;}
            private LogLevel level = null;

            //Defaults here
            public SingleConfig() {
                theLogger = "ConsoleLogger";
                theNamespace = null;
                filename = null;
                pattern = "{H:mm:ss.fff} %C [%L] $LINE";
                logLevel = "info"; 
            }

            //Extract the date pattern from the config
            public string GetDatePattern() {
                if (this.datePattern == null) {
                    Regex regex = new Regex("(?<=\\{).*?(?=\\})");
                    Match match = regex.Match(this.pattern);
                    if (match.Success) this.datePattern = match.Value;
                }
                return this.datePattern;
            }

            public LogLevel GetLogLevel() {
                if (this.level == null) this.level = LogLevel.FromString(this.logLevel);
                return this.level;
            }
        }

        public LoggerConfig() {
            loggerConfigs = new SingleConfig[0] {};
            defaultConfig = new SingleConfig();
        }

        public Logger GetLoggerForType(Type t) {
            //Search for this class's type in scope; use default if not found
            if (this.loggerConfigs != null) {
                foreach (SingleConfig singleConfig in this.loggerConfigs) {
                    if (!string.IsNullOrEmpty(singleConfig.theNamespace) &&
                        t.ToString().Contains(singleConfig.theNamespace)) {
                            return Logger.FromConfig(singleConfig, t);
                    }
                }
            }
            if (this.defaultConfig == null) this.defaultConfig = new SingleConfig();
            return Logger.FromConfig(this.defaultConfig, t);
        }

        public const int CLASS_NAME_LENGTH = 20;
    }
}
