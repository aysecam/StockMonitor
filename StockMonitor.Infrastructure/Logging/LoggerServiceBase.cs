using Serilog;
using StockMonitor.Application.Interfaces.Services;

namespace StockMonitor.Infrastructure.Logging;

public abstract class LoggerServiceBase
{
    protected ILogger Logger { get; set; }

    protected LoggerServiceBase()
    {
        Logger = Log.Logger;
    }

    protected LoggerServiceBase(ILogger logger)
    {
        Logger = logger;
    }
}