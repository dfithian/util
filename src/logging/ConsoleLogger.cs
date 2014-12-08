using System;

namespace Fithian.Logging
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(LoggerConfig.SingleConfig config, Type callingClass)
            : base(config, callingClass) {}
        public override void Write(string line) {
            Console.WriteLine(line);
        }
    }
}
