util
====

Configuration and logging library for C#

####Fithian.Logging

* Two types of Logger, FileLogger and ConsoleLogger
* Need a config file to load from
 * Default config filename is log.json and is searched for in the PATH environment variable directories
 * See test/src/Test.cs for an example of how to initialize the LoggerFactory
 * See test/src/logger.cfg and src/LoggerConfig.cs for an example of how to format the log config file (hint: use JSON)

```
LoggerFactory.FromFile(/*location of config*/);
Logger logger = LoggerFactory.GetLogger</*current class*/>();
```

####Fithian.Config

* Loads config from file and deserializes into a JSON-serializable type
* Needs to be provided with a file
* Will ask LoggerFactory for a logger if encounters an error
* See test/src/Test.cs for an example of how to initialize a config
* See test/src/test.cfg and test/src/TestConfig.cs for an example of how to format the config file (hint: use JSON)

```
namespace Test
{
    public class TestConfig
    {
        [JsonPropertyAttribute("stringField")]
        public string stringField {get; set;}
        [JsonPropertyAttribute("intField")]
        public int intField {get; set;}
        [JsonPropertyAttribute("boolField")]
        public bool boolField {get; set;}
        [JsonPropertyAttribute("arrayField")]
        public string[] arrayField {get; set;}

        //Add your defaults here
        public TestConfig() {
            this.stringField = "one";
            this.intField = 1;
            this.boolField = true;
            this.arrayField = new string[] {"one", "two"};
        }

        //Add a static method to your class to call into the config factory
        public static TestConfig FromFile(string filename, Logger logger) {
            return ConfigFactory<TestConfig>.FromFile(filename, logger);
        }
    }
}

namespace Test
{
	public class TestMain
	{
		static void Main(string[] args) {
			//Then just ask the class to create the config
			TestConfig testConfig = TestConfig.FromFile(/*path to file*/, /*logger*/)
		}
	}
}

```