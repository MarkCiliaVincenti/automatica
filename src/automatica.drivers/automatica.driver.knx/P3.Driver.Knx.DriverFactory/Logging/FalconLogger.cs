using Knx.Falcon.Logging;
using System;
using Microsoft.Extensions.Logging;

namespace P3.Driver.Knx.DriverFactory.Logging
{
    internal class FalconLogger(ILogger logger, string name) : IFalconLogger
    {
        public void Debug(object message)
        {
            logger.LogDebug($"{name}: {message}");
        }

        public void Debug(object message, Exception exception)
        {
            logger.LogDebug($"{name}: {message} {exception}");
        }

        public void DebugFormat(string format, params object[] args)
        {
            logger.LogDebug(format, args);
        }

        public void Error(object message)
        {
            logger.LogError($"{name}: {message}");
        }

        public void Error(object message, Exception exception)
        {
            logger.LogError($"{name}: {message} {exception}");
        }

        public void ErrorFormat(string format, params object[] args)
        {
            logger.LogError(format, args);
        }

        public void Info(object message)
        {
            logger.LogInformation($"{name}: {message}");
        }

        public void Info(object message, Exception exception)
        {
            logger.LogInformation($"{name}: {message} {exception}");
        }

        public void InfoFormat(string format, params object[] args)
        {
            logger.LogInformation(format, args);
        }

        public void Warn(object message)
        {
            logger.LogWarning($"{name}: {message}");
        }

        public void Warn(object message, Exception exception)
        {
            logger.LogWarning($"{name}: {message} {exception}");
        }

        public void WarnFormat(string format, params object[] args)
        {
            logger.LogWarning(format, args);
        }

        public bool IsDebugEnabled { get; } = true;
        public bool IsErrorEnabled { get; } = true;
        public bool IsInfoEnabled { get; } = true;
        public bool IsWarnEnabled { get; } = true;
    }
}
