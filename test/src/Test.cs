using System;
using Fithian.Logging;
using Fithian.Config;
using System.IO;

namespace Test
{
    public class TestMain
    {
        static void Main(string[] args) {
            LoggerFactory.FromFile(Directory.GetCurrentDirectory() + "/test/src/logger.cfg");
            Logger logger = LoggerFactory.GetLogger<TestMain>();
            logger.Debug("This is a test from Test!");
            TestConfig testConfig = TestConfig.FromFile(Directory.GetCurrentDirectory() + "/test/src/" + TestConfig.filename, logger);
            testConfig.Debug();
            logger.Trace("This is a TRACE log");
            logger.Debug("This is a DEBUG log");
            logger.Info("This is an INFO log");
            logger.Warn("This is a WARN log");
            logger.Error("This is an ERROR log");
            logger.Fatal("This is a FATAL log");
        }
    }
}
