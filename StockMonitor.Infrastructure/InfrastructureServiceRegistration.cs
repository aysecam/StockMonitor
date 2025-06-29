using Microsoft.Extensions.DependencyInjection;
using StockMonitor.Application.Interfaces.Services;
using StockMonitor.Infrastructure.Logging;
using StockMonitor.Infrastructure.Services;

namespace StockMonitor.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ILoggerService, SerilogLogger>();
        services.AddHttpClient<IFakeStoreService, FakeStoreService>();
        return services;
    }
}