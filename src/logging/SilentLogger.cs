using System;
using System.IO;

namespace Fithian.Logging
{
    public class SilentLogger : Logger
    {
        public SilentLogger(LoggerConfig config, Type callingClass)
            : base(config, callingClass) {}
        public override void Write(String line) { }
    }
}
