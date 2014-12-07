using System;

namespace Fithian.Logging
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(LoggerConfig config, Type callingClass)
            : base(config, callingClass) {}
        public override void Write(String line) {
            Console.Write(line);
        }
    }
}
