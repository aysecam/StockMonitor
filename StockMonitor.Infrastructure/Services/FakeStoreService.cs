using StockMonitor.Application.Dtos;
using StockMonitor.Application.Interfaces.Services;
using System.Net.Http.Json;

namespace StockMonitor.Infrastructure.Services;

public class FakeStoreService : IFakeStoreService
{
    private readonly HttpClient _httpClient;

    public FakeStoreService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://fakestoreapi.com/");
    }

    public async Task<List<FakeProductDto>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<FakeProductDto>>("products");
        return response ?? new List<FakeProductDto>();
    }

    public async Task<FakeProductDto?> GetCheapestByCodeAsync(string productCode)
    {
        var allProducts = await GetAllAsync();

        return allProducts
            .Where(p => p.Title.Contains(productCode, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Price)
            .FirstOrDefault();
    }
}