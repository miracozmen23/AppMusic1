using NLog;

namespace AppMusic1.Logger
{
    public class LoggerService : ILoggerService
    {
        private static readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        public void LogInfo(string message) =>  _logger.Info(message);

        public void LogWarn(string message) => _logger.Warn(message);

        public void LogDebug(string message) => _logger.Debug(message);

        public void LogError(string message) => _logger.Error(message);

    }
}










