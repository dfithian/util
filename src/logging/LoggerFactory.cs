using System;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace Fithian.Logging
{
    public static class LoggerFactory
    {
        private static LoggerConfig config;

        public static void FromFile(String configFilename) {
            if (!File.Exists(configFilename)) throw new FileNotFoundException("File " + configFilename + " doesn't exist");
            StreamReader sr = new StreamReader(configFilename);
            config = (LoggerConfig)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(LoggerConfig));
        }

        //Need to be able to initialize without finding a config file, but also without throwing exception later
        private static void InitializeFromDefault() {
            String[] allPaths = Environment.GetEnvironmentVariable("PATH").Split(new char[] {';', ':'});
            if (allPaths != null) {
                foreach (String path in allPaths) {
                    if (Directory.Exists(path)) {
                        String[] allFilenames = Directory.GetFiles(path);
                        if (allFilenames != null) {
                            foreach (String filename in allFilenames) {
                                if (filename == "log.json") {
                                    FromFile(path + "/" + filename);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            config = LoggerConfig.SilentConfig();
        }

        public static Logger GetLogger<T>() {
            if (config == null || config.filename == null) InitializeFromDefault();
            ConstructorInfo c = config.GetLogType().GetConstructor(new Type[] {typeof(LoggerConfig), typeof(Type)});
            return (Logger)c.Invoke(new object[] {config, typeof(T)});
        }
    }
}
