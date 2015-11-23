using System;

namespace openhabUWP.Interfaces.Services
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
}