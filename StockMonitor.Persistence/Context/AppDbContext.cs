using Microsoft.EntityFrameworkCore;
using StockMonitor.Domain.Entities;

namespace StockMonitor.Persistence.Context;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
}