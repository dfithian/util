using System;
using Fithian.Logging;
using Fithian.Config;
using System.IO;

namespace Test
{
    public class Test
    {
        static void Main(string[] args) {
            LoggerFactory.InitializeFromFile(Directory.GetCurrentDirectory() + "/test/src/logger.cfg");
            Logger logger = LoggerFactory.GetLogger(typeof(Test));
            logger.Debug("This is a test from Test!");
            TestConfig testConfig = Config.FromFile<TestConfig>(Directory.GetCurrentDirectory() + "/test/src/" + TestConfig.filename, logger);
            testConfig.Debug();
        }
    }
}
