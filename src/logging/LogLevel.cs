using System;

namespace Fithian.Logging
{
    public class LogLevel {
        private LogLevel(string value, int severity) { Value = value; Severity = severity;}
        public string Value {get; set;}
        public int Severity {get; set;}

        private const string TRACE = "TRACE";
        private const string DEBUG = "DEBUG";
        private const string INFO  = "INFO ";
        private const string WARN  = "WARN ";
        private const string ERROR = "ERROR";
        private const string FATAL = "FATAL";
        public const int TRACE_SEVERITY = 0;
        public const int DEBUG_SEVERITY = 1;
        public const int INFO_SEVERITY  = 2;
        public const int WARN_SEVERITY  = 3;
        public const int ERROR_SEVERITY = 4;
        public const int FATAL_SEVERITY = 5;
        public static LogLevel Trace { get { return new LogLevel(TRACE, TRACE_SEVERITY); } }
        public static LogLevel Debug { get { return new LogLevel(DEBUG, DEBUG_SEVERITY); } }
        public static LogLevel Info  { get { return new LogLevel(INFO,  INFO_SEVERITY);  } }
        public static LogLevel Warn  { get { return new LogLevel(WARN,  WARN_SEVERITY);  } }
        public static LogLevel Error { get { return new LogLevel(ERROR, ERROR_SEVERITY); } }
        public static LogLevel Fatal { get { return new LogLevel(FATAL, FATAL_SEVERITY); } }

        public static LogLevel FromString(string level) {
            if (level == null) level = "";
            switch (level.ToUpper()) {
                case TRACE: return Trace;
                case DEBUG: return Debug;
                case INFO:  return Info;
                case WARN:  return Warn;
                case ERROR: return Error;
                case FATAL: return Fatal;
                default: return Info;
            }
        }
    }
}
