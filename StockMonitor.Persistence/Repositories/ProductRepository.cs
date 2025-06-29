using Microsoft.EntityFrameworkCore;
using StockMonitor.Domain.Entities;
using StockMonitor.Domain.Interfaces;
using StockMonitor.Persistence.Context;

namespace StockMonitor.Persistence.Repositories;

public class ProductRepository : EfRepositoryBase<Product, Guid>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<List<Product>> GetLowStockProductsAsync()
    {
        return await _dbSet
            .Where(p => p.Stock < p.Threshold)
            .ToListAsync();
    }
}