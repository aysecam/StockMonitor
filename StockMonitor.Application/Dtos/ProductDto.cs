namespace StockMonitor.Application.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int Threshold { get; set; }
}