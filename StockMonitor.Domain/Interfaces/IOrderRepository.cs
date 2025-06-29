using StockMonitor.Domain.Entities;

namespace StockMonitor.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order, Guid>
{
    
}