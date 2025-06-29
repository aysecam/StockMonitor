using StockMonitor.Domain.Entities;

namespace StockMonitor.Domain.Interfaces;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<List<Product>> GetLowStockProductsAsync();
}