namespace StockMonitor.Application.Dtos;

public class FakeProductDto
{
    public int Id { get; set; }                // Fake Store ID
    public string Title { get; set; } = "";    // Ürün adı (eşleştirme için)
    public decimal Price { get; set; }         // Fiyat
    public string Description { get; set; } = "";
    public string Image { get; set; } = "";
}