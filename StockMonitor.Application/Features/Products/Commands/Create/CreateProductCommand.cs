using MediatR;
using StockMonitor.Application.Common.Behaviors.Transaction;
using StockMonitor.Domain.Entities;
using StockMonitor.Domain.Interfaces;

namespace StockMonitor.Application.Features.Products.Commands.Create;

public class CreateProductCommand :IRequest<Guid>, ITransactionalRequest // başarılı olursa yeni ürünün ID'sini dönecek
{
    public string Name { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int Threshold { get; set; }
    
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                ProductCode = request.ProductCode,
                Stock = request.Stock,
                Threshold = request.Threshold
            };

            await _productRepository.AddAsync(product);
            return product.Id;
        }
    }
}