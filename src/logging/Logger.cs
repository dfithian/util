using System;

namespace Fithian.Logging
{
    public abstract class Logger
    {
        protected Type callingClass;
        protected LoggerConfig config;
        public Logger(LoggerConfig config, Type callingClass) {
            this.config = config;
            this.callingClass = callingClass;
            if (this.config == null) throw new NullReferenceException("Config was null. Cannot create Logger.");
            if (string.IsNullOrEmpty(this.config.filename)) throw new NullReferenceException("Filename was null. Cannot create Logger.");
            if (this.callingClass == null) throw new NullReferenceException("Calling class was null. Cannot create Logger.");
        }
        public void Info(String line) {
            this.Write(DateTime.Now.ToString() + " " + this.callingClass.ToString() + " [INFO]  " + line + "\n");
        }
        public void Debug(String line) {
            if (this.config.debugEnabled) this.Write(DateTime.Now.ToString() + " " + this.callingClass.ToString() + " [DEBUG] " + line + "\n");
        }
        public void Error(String line) {
            this.Write(DateTime.Now.ToString() + " " + this.callingClass.ToString() + " [ERROR] " + line + "\n");
        }
        public void Error(String line, Exception e) {
            this.Write(DateTime.Now.ToString() + " " + this.callingClass.ToString() + " [ERROR] " + line + "\n");
            this.Write(DateTime.Now.ToString() + " " + this.callingClass.ToString() + " [ERROR] " + e.StackTrace + "\n");
        }
        public abstract void Write(String line);
    }
}
