﻿using System;
using openhabUWP.Enums;

namespace openhabUWP.Services
{
    public interface ILogService
    {
        void Info(string message);
        void Info(string message, params object[] args);

        void Debug(string message);
        void Debug(string message, params object[] args);

        void Warn(string message);
        void Warn(string message, params object[] args);

        void Error(string message);
        void ErrorFormat(string message, params object[] args);
        void Error(Exception exception);
    }

    public class LogService : ILogService
    {
        private const string LOG_PATTERN = "{0:s}\t{1}\t{2}";
        private void Write(DateTime timestamp = default(DateTime), LogType type = LogType.DEBUG, object content = default(object))
        {
            System.Diagnostics.Debug.WriteLine(LOG_PATTERN, timestamp, type, content);
        }

        public void Info(string message)
        {
            Write(DateTime.Now, LogType.INFO, message);
        }

        public void Info(string message, params object[] args)
        {
            Write(DateTime.Now, LogType.INFO, string.Format(message, args));
        }

        public void Debug(string message)
        {
            Write(DateTime.Now, LogType.DEBUG, message);
        }

        public void Debug(string message, params object[] args)
        {
            Write(DateTime.Now, LogType.DEBUG, string.Format(message, args));
        }

        public void Warn(string message)
        {
            Write(DateTime.Now, LogType.WARN, message);
        }

        public void Warn(string message, params object[] args)
        {
            Write(DateTime.Now, LogType.WARN, string.Format(message, args));
        }

        public void Error(string message)
        {
            Write(DateTime.Now, LogType.ERROR, message);
        }

        public void ErrorFormat(string message, params object[] args)
        {
            Write(DateTime.Now, LogType.ERROR, string.Format(message, args));
        }

        public void Error(Exception exception)
        {
            Write(DateTime.Now, LogType.ERROR, string.Concat(exception.Message, " - ", exception.StackTrace));
        }
    }
}
