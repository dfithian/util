using System;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace Fithian.Logging
{
    public static class LoggerFactory
    {
        private static LoggerConfig config;

        public static void InitializeFromFile(String configFilename) {
            if (!File.Exists(configFilename)) throw new FileNotFoundException("File " + configFilename + " doesn't exist");
            StreamReader sr = new StreamReader(configFilename);
            config = (LoggerConfig)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(LoggerConfig));
        }

        private static void InitializeFromDefault() {
            throw new FileNotFoundException("Not yet implemented; no file from which to initialize");
        }

        public static Logger GetLogger(Type callingClass) {
            if (string.IsNullOrEmpty(config.filename)) InitializeFromDefault();
            ConstructorInfo c = config.GetLogType().GetConstructor(new Type[] {typeof(LoggerConfig), typeof(Type)});
            return (Logger)c.Invoke(new object[] {config, callingClass});
        }

        public static Logger GetLogger() {
            return GetLogger(config.GetLogType());
        }
    }
}
