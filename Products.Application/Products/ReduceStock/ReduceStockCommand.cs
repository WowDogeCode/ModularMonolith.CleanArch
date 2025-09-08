using MediatR;

namespace Products.Application.Products.ReduceStock
{
    public record ReduceStockCommand : IRequest<bool>
    {
        public int ProductId { get; init; }
        public short Quantity { get; init; }
    }
}
