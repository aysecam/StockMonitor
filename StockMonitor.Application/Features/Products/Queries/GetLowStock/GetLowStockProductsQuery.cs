using MediatR;
using StockMonitor.Application.Dtos;
using StockMonitor.Domain.Interfaces;

namespace StockMonitor.Application.Features.Products.Queries.GetLowStock;

public class GetLowStockProductsQuery : IRequest<List<ProductDto>>
{
    
    public class GetLowStockProductsHandler : IRequestHandler<GetLowStockProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetLowStockProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
        {
            var lowStockProducts = await _productRepository.GetLowStockProductsAsync();

            return lowStockProducts.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ProductCode = p.ProductCode,
                Stock = p.Stock,
                Threshold = p.Threshold
            }).ToList();
        }
    }
    
    
}