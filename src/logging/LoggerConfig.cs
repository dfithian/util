using System;
using Newtonsoft.Json;

namespace Fithian.Logging
{
    public class LoggerConfig
    {
        [JsonPropertyAttribute("filename")]
        public string filename;
        [JsonPropertyAttribute("debugEnabled")]
        public bool debugEnabled;
        [JsonPropertyAttribute("logType")]
        public string logType;
        private Type type = null;

        public Type GetLogType() {
            if (this.type == null) {
                if (string.IsNullOrEmpty(this.logType)) this.type = typeof(ConsoleLogger);
                else this.type = Type.GetType("Fithian.Logging." + this.logType);
                if (this.type == null) this.type = typeof(ConsoleLogger); //Still null
            }
            return this.type;
        }

        public static LoggerConfig SilentConfig() {
            LoggerConfig silentConfig = new LoggerConfig();
            silentConfig.filename = "";
            silentConfig.debugEnabled = false;
            silentConfig.logType = "SilentLogger";
            return silentConfig;
        }
    }
}
