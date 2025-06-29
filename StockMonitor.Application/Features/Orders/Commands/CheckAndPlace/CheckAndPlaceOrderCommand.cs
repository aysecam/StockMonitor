using MediatR;
using StockMonitor.Application.Common.Behaviors.Logging;
using StockMonitor.Application.Common.Behaviors.Transaction;
using StockMonitor.Application.Interfaces.Services;
using StockMonitor.Domain.Entities;
using StockMonitor.Domain.Interfaces;

namespace StockMonitor.Application.Features.Orders.Commands.CheckAndPlace;

public class CheckAndPlaceOrderCommand: IRequest<int> , ITransactionalRequest,ILoggableRequest
// başarıyla oluşturulan sipariş sayısı
{
    public class CheckAndPlaceOrderHandler : IRequestHandler<CheckAndPlaceOrderCommand, int>
    {
        public IProductRepository _productRepository { get; set; }
        public IOrderRepository _orderRepository { get; set; }
        public IFakeStoreService _fakeStoreService { get; set; }
        
        public CheckAndPlaceOrderHandler(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IFakeStoreService fakeStoreService)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _fakeStoreService = fakeStoreService;
        }
        public async Task<int> Handle(CheckAndPlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var lowStockProducts = await _productRepository.GetLowStockProductsAsync();

            int orderCount = 0;

            foreach (var product in lowStockProducts)
            {
                var cheapest = await _fakeStoreService.GetCheapestByCodeAsync(product.ProductCode);
                if (cheapest == null) continue;

                var order = new Order
                {
                    ProductId = product.Id,
                    SupplierProductId = cheapest.Id.ToString(),
                    Price = cheapest.Price,
                    OrderDate = DateTime.UtcNow
                };

                await _orderRepository.AddAsync(order);
                orderCount++;
            }

            return orderCount;        }
    }
}