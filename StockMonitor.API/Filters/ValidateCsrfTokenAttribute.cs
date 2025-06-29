using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StockMonitor.API.Filters;

public class ValidateCsrfTokenAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;
        var path = request.Path.Value ?? "";

        // Swagger'dan gelen istekleri tamamen muaf tut
        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
            return;

        // Sadece durum değiştirici HTTP metotlarında CSRF doğrulaması yap
        if (HttpMethods.IsGet(request.Method) ||
            HttpMethods.IsHead(request.Method) ||
            HttpMethods.IsOptions(request.Method))
        {
            return; // CSRF gereksiz
        }

        var antiforgery = context.HttpContext.RequestServices.GetRequiredService<IAntiforgery>();

        try
        {
            await antiforgery.ValidateRequestAsync(context.HttpContext);
        }
        catch
        {
            context.Result = new BadRequestObjectResult(new
            {
                error = "CSRF validation failed."
            });
        }
    }
}
