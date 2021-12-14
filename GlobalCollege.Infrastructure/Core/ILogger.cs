using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Infrastructure.Core
{
    public interface ILogger
    {
        void Log(LogEntry entry);
    }

    public enum LoggingEventType { Debug, Information, Warning, Error, Fatal };

    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;

        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {

            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
        }
    }
}
