using StockMonitor.Domain.Common;
namespace StockMonitor.Domain.Interfaces;

public interface IRepository<TEntity, TId> 
    where TEntity : Entity<TId>
{
    Task<TEntity?> GetByIdAsync(TId id);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}