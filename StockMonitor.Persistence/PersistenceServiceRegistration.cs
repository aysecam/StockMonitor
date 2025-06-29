using Microsoft.Extensions.DependencyInjection;
using StockMonitor.Domain.Interfaces;
using StockMonitor.Persistence.Context;
using StockMonitor.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace StockMonitor.Persistence;
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("StockMonitorDb"));

        // Repos
        services.AddScoped(typeof(IRepository<,>), typeof(EfRepositoryBase<,>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}