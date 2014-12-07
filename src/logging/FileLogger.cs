using System;
using System.IO;

namespace Fithian.Logging
{
    public class FileLogger : Logger
    {
        public FileLogger(LoggerConfig config, Type callingClass)
            : base(config, callingClass) {}
        public override void Write(String line) {
            using (StreamWriter sw = File.AppendText(this.config.filename)) {
                sw.Write(line);
            }
        }
    }
}
