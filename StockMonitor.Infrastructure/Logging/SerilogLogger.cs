using StockMonitor.Application.Interfaces.Services;

namespace StockMonitor.Infrastructure.Logging;

public class SerilogLogger : LoggerServiceBase, ILoggerService
{
    public void Info(string message) => Logger.Information(message);
    public void Warn(string message) => Logger.Warning(message);
    public void Debug(string message) => Logger.Debug(message);
    public void Error(string message) => Logger.Error(message);
    public void Fatal(string message) => Logger.Fatal(message);
    public void Verbose(string message) => Logger.Verbose(message);
}