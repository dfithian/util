util
====

Configuration and logging library for C#

####Fithian.Logging

* Two types of Logger, FileLogger and ConsoleLogger
* Need a config file to load from
 * Default config location is not yet implemented
 * See test/src/Test.cs for an example of how to initialize the LoggerFactory
 * See test/src/logger.cfg and src/LoggerConfig.cs for an example of how to format the log config file (hint: use JSON)

```csharp
LoggerFactory.InitializeFromFile(/*location of config*/); //Will be unneeded in the future potentially
Logger logger = LoggerFactory.GetLogger</*current class*/>();
```

####Fithian.Config

* Loads config from file and deserializes into a JSON-serializable type
* Needs to be provided with a file
* Will ask LoggerFactory for a logger if encounters an error
* See test/src/Test.cs for an example of how to initialize a config
* See test/src/test.cfg and test/src/TestConfig.cs for an example of how to format the config file (hint: use JSON)

```csharp
TestConfig config = Config.FromFile<TestConfig>(/*location of config*/);
```