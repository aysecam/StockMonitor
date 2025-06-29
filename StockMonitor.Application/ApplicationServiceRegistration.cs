using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StockMonitor.Application.Common.Behaviors.Logging;
using StockMonitor.Application.Common.Behaviors.Transaction;

namespace StockMonitor.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionScopeBehavior<,>));

        return services;
    }
}