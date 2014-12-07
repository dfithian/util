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
        [JsonPropertyAttribute("defaultLogger")]
        private string defaultLogger;
        private Type defaultLoggerType;
        [JsonPropertyAttribute("loggers")]
        private LoggerType[] loggers {get; set;}
        private class LoggerType
        {
            [JsonPropertyAttribute("logger")]
            public string theLogger {get; set;}
            [JsonPropertyAttribute("namespace")]
            public string theNamespace {get; set;}
            public Type theType = null;
        }

        public Type GetLogType(Type t) {
            if (this.loggers != null) {
                foreach (LoggerType loggerType in this.loggers) {
                    if (t.ToString().Contains(loggerType.theNamespace)) {
                        if (loggerType.theType == null) loggerType.theType = Type.GetType("Fithian.Logging." + loggerType.theLogger);
                        if (loggerType.theType != null) return loggerType.theType;
                    }
                }
            }
            if (this.defaultLoggerType == null) this.defaultLoggerType = Type.GetType("Fithian.Logging" + this.defaultLogger);
            if (this.defaultLoggerType != null) return this.defaultLoggerType;
            return typeof(ConsoleLogger);
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
