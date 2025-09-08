using Common.Application.Abstraction;
using MediatR;
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
        public async Task ReduceStockAsync(int productId, short quantity, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ReduceStockCommand { ProductId = productId, Quantity = quantity }, cancellationToken);
        }
    }
}
