using StockMonitor.Domain.Common;

namespace StockMonitor.Domain.Entities;

public class Product:Entity<Guid>
{
    public string Name { get; set; } = null!;
    public string ProductCode { get; set; } = null!; // Fake Store ile eşleşme
    public int Stock { get; set; }
    public int Threshold { get; set; } // Kritik stok seviyesi
}