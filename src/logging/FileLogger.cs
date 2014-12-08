using System;
using System.IO;

namespace Fithian.Logging
{
    public class FileLogger : Logger
    {
        public FileLogger(LoggerConfig.SingleConfig config, Type callingClass) : base(config, callingClass) {
            if (string.IsNullOrEmpty(this.config.filename)) throw new NullReferenceException("Filename was null. Cannot create Logger.");
        }
        public override void Write(string line) {
            using (StreamWriter sw = File.AppendText(this.config.filename)) {
                sw.WriteLine(line);
            }
        }
    }
}
