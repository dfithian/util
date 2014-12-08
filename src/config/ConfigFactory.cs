using System;
using System.IO;
using Newtonsoft.Json;
using Fithian.Logging;
using System.Reflection;

namespace Fithian.Config
{
    public abstract class ConfigFactory<T>
    {
        public static T FromFile(string filename, Logger logger) {
            T config = default(T);
            try {
                StreamReader sr = new StreamReader(filename);
                config = (T)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(T));
            } catch (Exception e) {
                if (logger != null) logger.Error("Exception encountered loading config", e);
            }
            return config;
        }
    }
}
