using StockMonitor.Application.Dtos;

namespace StockMonitor.Application.Interfaces.Services;

public interface IFakeStoreService
{
    Task<List<FakeProductDto>> GetAllAsync();
    Task<FakeProductDto?> GetCheapestByCodeAsync(string productCode);
}