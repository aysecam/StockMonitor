using System.Text.RegularExpressions;
namespace StockMonitor.API.Middlewares;

public class XssSanitizerMiddleware
{
    private readonly RequestDelegate _next;

    public XssSanitizerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        foreach (var key in context.Request.Query.Keys)
        {
            var original = context.Request.Query[key];
            var sanitized = Sanitize(original);
            if (original != sanitized)
            {
                Console.WriteLine($"XSS attempt blocked in query: {key} = {original}");
            }
        }

        // ✅ Form sanitize
        if (context.Request.HasFormContentType)
        {
            var form = await context.Request.ReadFormAsync();
            var sanitizedForm = form.Keys.ToDictionary(
                key => key,
                key => (Microsoft.Extensions.Primitives.StringValues)Sanitize(form[key])
            );

            context.Request.Form = new FormCollection(sanitizedForm);
        }

        await _next(context);
    }

    private string Sanitize(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;
        return Regex.Replace(input, "<.*?>", string.Empty);
    }
}