using StockMonitor.Domain.Entities;
using StockMonitor.Persistence.Context;

namespace StockMonitor.Persistence;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Products.Any())
            return; // Zaten veri var

        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bluetooth Speaker",
                ProductCode = "speaker",
                Stock = 5,
                Threshold = 10
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Gaming Mouse",
                ProductCode = "mouse",
                Stock = 12,
                Threshold = 15
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "USB Cable",
                ProductCode = "usb",
                Stock = 1,
                Threshold = 5
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}