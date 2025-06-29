using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using StockMonitor.API.Middlewares;
using StockMonitor.Application;
using StockMonitor.Infrastructure;
using StockMonitor.Persistence;
using StockMonitor.Persistence.Context;


var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN"; // Angular & Postman uyumu için bu isim en yaygını
});
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);  // 1 dakika sürede
        opt.PermitLimit = 10;                  // En fazla 10 istek
        opt.QueueLimit = 0;                    // Kuyruk yok
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockMonitor API", Version = "v1" });

    // CSRF Token için manuel header tanımı
    c.AddSecurityDefinition("X-XSRF-TOKEN", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-XSRF-TOKEN",
        Type = SecuritySchemeType.ApiKey,
        Description = "CSRF token. Cookie'deki XSRF-TOKEN değeri buraya yazılmalı."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "X-XSRF-TOKEN",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-XSRF-TOKEN"
                }
            },
            new List<string>()
        }
    });
});
var app = builder.Build();
//Seed işlemi
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(db);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<XssSanitizerMiddleware>();
app.Use(async (context, next) =>
{
    var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
    var tokens = antiforgery.GetAndStoreTokens(context);
    
    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!, new CookieOptions
    {
        HttpOnly = false, // JavaScript okuyabilsin
        SameSite = SameSiteMode.Strict,
        Secure = true // HTTPS zorunlu
    });

    await next(context);
});
app.UseRateLimiter(); 

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();