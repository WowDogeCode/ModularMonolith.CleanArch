using Common.Application.Abstraction;
using Common.Application.DTOs;
using MediatR;
using Products.Application.Products.GetProductsInventoryInfo;
using Products.Application.Products.ReduceStock;

namespace Products.Application.Services
{
    public sealed class InventoryService : IInventoryService
    {
        private readonly IMediator _mediator;
        public InventoryService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<ProductInventorySnapshotDto>> GetProductInventorySnapshotsAsync(List<int> productIds, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetProductInventorySnapshotsQuery { ProductIds = productIds }, cancellationToken);

            return response;
        }
        public async Task<bool> ReduceStockAsync(int productId, short quantity, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ReduceStockCommand { ProductId = productId, Quantity = quantity }, cancellationToken);
            
            return result;
        }
    }
}
