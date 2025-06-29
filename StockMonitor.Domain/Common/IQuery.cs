namespace StockMonitor.Domain.Common;

public interface IQuery<T>
{
    IQueryable<T> Query();
}
