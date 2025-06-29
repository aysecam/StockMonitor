using MediatR;
using StockMonitor.Application.Interfaces.Services;

namespace StockMonitor.Application.Common.Behaviors.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILoggerService _logger;

    public LoggingBehavior(ILoggerService logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is ILoggableRequest)
        {
            var requestName = typeof(TRequest).Name;
            _logger.Info($"[LOG] Handling request: {requestName}");
            _logger.Debug($"[LOG] Request data: {System.Text.Json.JsonSerializer.Serialize(request)}");
        }

        var response = await next();

        if (request is ILoggableRequest)
        {
            var requestName = typeof(TRequest).Name;
            _logger.Info($"[LOG] Request handled: {requestName}");
        }

        return response;
    }
}