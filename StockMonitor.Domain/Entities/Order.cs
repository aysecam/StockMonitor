using StockMonitor.Domain.Common;

namespace StockMonitor.Domain.Entities;

public class Order :Entity<Guid>
{
    public Guid ProductId { get; set; }                  // Bizim ürünümüzle eşleşme
    public string SupplierProductId { get; set; } = "";  // Fake Store'daki ürünün ID'si
    public decimal Price { get; set; }                   // Satın alma fiyatı
    public DateTime OrderDate { get; set; }              // Sipariş oluşturulma zamanı
    public Product? Product { get; set; }
}