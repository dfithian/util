using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Fithian.Logging
{
    public abstract class Logger
    {
        protected Type callingClass {get; set;}
        protected LoggerConfig.SingleConfig config {get; set;}
        protected Logger(LoggerConfig.SingleConfig config, Type callingClass) {
            this.config = config;
            this.callingClass = callingClass;
            if (this.config == null) throw new NullReferenceException("Config was null. Cannot create Logger.");
            if (this.callingClass == null) throw new NullReferenceException("Calling class was null. Cannot create Logger.");
        }
        public void Trace(string line) {
            if (this.config.GetLogLevel().Severity <= LogLevel.TRACE_SEVERITY) {
                this.Write(this.Format(line, LogLevel.Warn));
            }
        }
        public void Debug(string line) {
            if (this.config.GetLogLevel().Severity <= LogLevel.DEBUG_SEVERITY) {
                this.Write(this.Format(line, LogLevel.Debug));
            }
        }
        public void Info(string line) {
            if (this.config.GetLogLevel().Severity <= LogLevel.INFO_SEVERITY) {
                this.Write(this.Format(line, LogLevel.Info));
            }
        }
        public void Warn(string line) {
            if (this.config.GetLogLevel().Severity <= LogLevel.WARN_SEVERITY) {
                this.Write(this.Format(line, LogLevel.Warn));
            }
        }
        public void Error(string line) {
            if (this.config.GetLogLevel().Severity <= LogLevel.ERROR_SEVERITY) {
                this.Write(this.Format(line, LogLevel.Error));
            }
        }
        public void Error(string line, Exception e) {
            if (this.config.GetLogLevel().Severity <= LogLevel.ERROR_SEVERITY) {
                this.Write(this.Format(line, LogLevel.Error));
                this.Write(this.Format(e.StackTrace, LogLevel.Error));
            }
        }
        public void Fatal(string line) {
            if (this.config.GetLogLevel().Severity <= LogLevel.FATAL_SEVERITY) {
                this.Write(this.Format(line, LogLevel.Fatal));
            }
        }
        public void Fatal(string line, Exception e) {
            if (this.config.GetLogLevel().Severity <= LogLevel.FATAL_SEVERITY) {
                this.Write(this.Format(line, LogLevel.Fatal));
                this.Write(this.Format(e.StackTrace, LogLevel.Fatal));
            }
        }
        private string Format(string line, LogLevel logLevel) {
            string className = this.callingClass.ToString();
            //Stretch or shrink class name to fit within 20 characters
            if (className.Length > LoggerConfig.CLASS_NAME_LENGTH) {
                className = className.Substring(className.Length - LoggerConfig.CLASS_NAME_LENGTH, LoggerConfig.CLASS_NAME_LENGTH);
            } else {
                className += new string(' ', LoggerConfig.CLASS_NAME_LENGTH - className.Length);
            }
            DateTime dateTime = DateTime.Now;
            string datePattern = this.config.GetDatePattern();
            return this.config.pattern.Replace("{"+datePattern+"}", dateTime.ToString(datePattern)) //Format date based on config
                .Replace("%C", className) //Insert class name if it's in the pattern
                .Replace("%L", logLevel.Value) //Replace log level if it's in the pattern
                + line; //Append the line at the end
        }
        public abstract void Write(string line);

        public static Logger FromConfig(LoggerConfig.SingleConfig config, Type callingClass) {
            if (config == null || config.theLogger == null) return new ConsoleLogger(config, callingClass);
            switch (config.theLogger.ToLower()) {
                case "consolelogger": return new ConsoleLogger(config, callingClass);
                case "filelogger": return new FileLogger(config, callingClass);
                default: return new ConsoleLogger(config, callingClass);
            }
        }
    }
}
