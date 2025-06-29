namespace StockMonitor.Application.Interfaces.Services;

public interface ILoggerService
{
    void Info(string message);
    void Warn(string message);
    void Debug(string message);
    void Error(string message);
    void Fatal(string message);
    void Verbose(string message);
}