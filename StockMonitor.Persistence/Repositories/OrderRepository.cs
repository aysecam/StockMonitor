using StockMonitor.Domain.Entities;
using StockMonitor.Domain.Interfaces;
using StockMonitor.Persistence.Context;

namespace StockMonitor.Persistence.Repositories;

public class OrderRepository : EfRepositoryBase<Order, Guid>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

}