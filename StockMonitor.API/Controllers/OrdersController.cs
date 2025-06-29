using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using StockMonitor.API.Filters;
using StockMonitor.Application.Features.Orders.Commands.CheckAndPlace;

namespace StockMonitor.API.Controllers;


public class OrdersController : BaseController
{

    [HttpPost("check-and-place")]
    [EnableRateLimiting("fixed")]
    public async Task<IActionResult> CheckAndPlaceOrders()
    {
        var result = await Mediator.Send(new CheckAndPlaceOrderCommand());
        return Ok(new { CreatedOrderCount = result });
    }
}