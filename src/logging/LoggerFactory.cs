using System;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace Fithian.Logging
{
    public static class LoggerFactory
    {
        private static LoggerConfig config {get; set;}
        private static bool initialized = false;

        public static void FromFile(string configFilename) {
            initialized = false;
            if (!File.Exists(configFilename)) throw new FileNotFoundException("File " + configFilename + " doesn't exist");
            StreamReader sr = new StreamReader(configFilename);
            config = (LoggerConfig)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(LoggerConfig));
            initialized = true;
        }

        //FIXME add ability to look up a logger by type (i.e. enumerate which namespaces get FileLogger in config)
        public static Logger GetLogger<T>() {
            if (!initialized) throw new InvalidOperationException("LoggerFactory has not been properly initialized");
            ConstructorInfo c = config.GetLogType(typeof(T)).GetConstructor(new Type[] {typeof(LoggerConfig), typeof(Type)});
            return (Logger)c.Invoke(new object[] {config, typeof(T)});
        }
    }
}
