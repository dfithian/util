using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Fithian.Logging
{
    public class LoggerConfig
    {
        [JsonPropertyAttribute("filename")]
        public string filename {get; set;}
        [JsonPropertyAttribute("pattern")]
        public string pattern {get; set;}
        private string datePattern = null;
        [JsonPropertyAttribute("level")]
        public string logLevel {get; set;}
        private LogLevel level = null;
        [JsonPropertyAttribute("type")]
        public string logType {get; set;}
        private Type type = null;

        public Type GetLogType() {
            if (this.type == null) {
                if (string.IsNullOrEmpty(this.logType)) this.type = typeof(ConsoleLogger);
                else this.type = Type.GetType("Fithian.Logging." + this.logType);
                if (this.type == null) this.type = typeof(ConsoleLogger); //Still null
            }
            return this.type;
        }

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

        public const string DEFAULT_PATTERN = "{H:mm:ss.fff} %C [%L] $LINE";
        public const int CLASS_NAME_LENGTH = 20;
    }
}
