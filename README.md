# StockMonitor
StockMonitor is a modern inventory tracking system that monitors product stock levels and automatically places orders from the most affordable supplier (Fake Store API) when stock falls below a critical threshold. Built using .NET 8, Clean Architecture, CQRS, and secured with anti-CSRF and XSS protection.

**Features**

-Manage your internal product catalog with stock & threshold values
-Detect low-stock items
-Automatically place orders from the cheapest Fake Store supplier
-API security: rate limiting, CSRF protection, and XSS sanitization
-Bonus request is at the end of the README.MD file.

**Clean Architecture Layers**
-StockMonitor.API: Entry point, controllers, middleware
-StockMonitor.Application: CQRS, DTOs, MediatR handlers, business logic
-StockMonitor.Domain: Core entities, interfaces, enums
-StockMonitor.Persistence: EF Core data access, repositories, DbContext
-StockMonitor.Infrastructure: External services (FakeStore), logging, etc.

**How to Run**
1-Clone the repository:

git clone https://github.com/your-username/StockMonitor.git
cd StockMonitor

2-Restore dependencies:

dotnet restore

3-Run the project:

dotnet run --project StockMonitor.API

3-Open Swagger:

http://localhost:5062/swagger

**CSRF Token Handling**

-All POST requests require a CSRF token (X-XSRF-TOKEN header).
-When using Swagger:
1. First, make a GET request (e.g., /api/Products) to receive the XSRF-TOKEN cookie.
2. Copy the cookie's value.
3. Paste it as the X-XSRF-TOKEN header in your POST requests via Swagger.


**API Endpoints**

1. Add Product
POST /api/Products

Request Body:
{
  "name": "Macbook Pro",
  "productCode": "macbook",
  "stock": 10,
  "threshold": 5
}

2.  List Low-Stock Products
GET /api/Products/low-stock

3.  Auto-Order Low-Stock Products
POST /api/Orders/check-and-place
Automatically finds the cheapest matching product from Fake Store and places an order.


**Tech Stack**
.NET 8
ASP.NET Core Web API
MediatR (CQRS)
Entity Framework Core (In-Memory)
Serilog
Swagger UI
HttpClient (for FakeStore integration)
Rate Limiting (Fixed Window)
CSRF Protection
XSS Sanitization Middleware

**Fake Store API Reference**

📡 https://fakestoreapi.com/products
Read-only access, no support for product creation or updates.
Used to simulate external supplier integration.

**Security Highlights**
Rate Limiting: Fixed window limiting per endpoint
CSRF Protection: Required token on all unsafe HTTP methods
XSS Protection: Middleware to sanitize query/form parameters

**📎 Developer Notes**
Uses custom middleware for XSS filtering
CSRF protection works seamlessly with Swagger (via manual header injection)
Follows SOLID principles and extensible architecture
   

**Bonus**

<!DOCTYPE html>
<html lang="tr">
<head>
  <meta charset="UTF-8">
  <title>Roma Rakamı ile Gösterim</title>
</head>
<body>

<div id="stockDisplay"></div>

<script>
  function toRoman(num) {
    const romanMap = [
      { value: 1000, symbol: 'M' },
      { value: 900, symbol: 'CM' },
      { value: 500, symbol: 'D' },
      { value: 400, symbol: 'CD' },
      { value: 100, symbol: 'C' },
      { value: 90, symbol: 'XC' },
      { value: 50, symbol: 'L' },
      { value: 40, symbol: 'XL' },
      { value: 10, symbol: 'X' },
      { value: 9, symbol: 'IX' },
      { value: 5, symbol: 'V' },
      { value: 4, symbol: 'IV' },
      { value: 1, symbol: 'I' }
    ];

    let result = '';
    for (const { value, symbol } of romanMap) {
      while (num >= value) {
        result += symbol;
        num -= value;
      }
    }
    return result;
  }

  // Örnek stok miktarı gösterimi
  const stock = 4;
  const romanStock = toRoman(stock);

  document.getElementById('stockDisplay').innerText = `Stok: ${romanStock} adet`;
</script>

</body>
</html>

