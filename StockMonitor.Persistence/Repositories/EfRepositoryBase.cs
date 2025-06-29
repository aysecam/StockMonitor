using StockMonitor.Domain.Common;
using Microsoft.EntityFrameworkCore;
using StockMonitor.Domain.Interfaces;
using StockMonitor.Persistence.Context;

namespace StockMonitor.Persistence.Repositories;

public class EfRepositoryBase<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public EfRepositoryBase(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
        => await _dbSet.FindAsync(id);

    public async Task<List<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}