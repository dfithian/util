using System;
using Fithian.Logging;
using Fithian.Config;
using System.IO;

namespace Test
{
    public class Test
    {
        static void Main(string[] args) {
            Logger silentLogger = LoggerFactory.GetLogger<Test>();
            LoggerFactory.FromFile(Directory.GetCurrentDirectory() + "/test/src/logger.cfg");
            Logger logger = LoggerFactory.GetLogger<Test>();
            logger.Info("Created a silent logger first... I think. Type is " + silentLogger.GetType().ToString());
            logger.Debug("This is a test from Test!");
            TestConfig testConfig = Config.FromFile<TestConfig>(Directory.GetCurrentDirectory() + "/test/src/" + TestConfig.filename, logger);
            testConfig.Debug();
        }
    }
}
