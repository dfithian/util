using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Fithian.Logging
{
    public abstract class Logger
    {
        protected Type callingClass {get; set;}
        protected LoggerConfig config {get; set;}
        public Logger(LoggerConfig config, Type callingClass) {
            this.config = config;
            this.callingClass = callingClass;
            if (this.config == null) throw new NullReferenceException("Config was null. Cannot create Logger.");
            if (this.config.filename == null) throw new NullReferenceException("Filename was null. Cannot create Logger.");
            if (this.config.pattern == null) this.config.pattern = LoggerConfig.DEFAULT_PATTERN;
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
            if (className.Length > LoggerConfig.CLASS_NAME_LENGTH) {
                className = className.Substring(className.Length - LoggerConfig.CLASS_NAME_LENGTH, LoggerConfig.CLASS_NAME_LENGTH);
            } else {
                className += new string(' ', LoggerConfig.CLASS_NAME_LENGTH - className.Length);
            }
            DateTime dateTime = DateTime.Now;
            string datePattern = this.config.GetDatePattern();
            return this.config.pattern.Replace("{"+datePattern+"}", dateTime.ToString(datePattern))
                .Replace("%C", className)
                .Replace("%L", logLevel.Value)
                .Replace("$LINE", line);
        }
        public abstract void Write(string line);
    }
}
