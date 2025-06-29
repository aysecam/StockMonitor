using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using StockMonitor.Application.Features.Products.Commands.Create;
using StockMonitor.Application.Features.Products.Queries.GetLowStock;

namespace StockMonitor.API.Controllers;

public class ProductsController:BaseController
{
    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStockProducts()
    {
        var result = await Mediator.Send(new GetLowStockProductsQuery());
        return Ok(result);
    }
    [HttpPost]
    [EnableRateLimiting("fixed")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var productId = await Mediator.Send(command);
        return Ok(new { ProductId = productId });
    }

}