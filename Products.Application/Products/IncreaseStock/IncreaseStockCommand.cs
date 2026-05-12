using MediatR;

namespace Products.Application.Products.IncreaseStock
{
    public record IncreaseStockCommand : IRequest<bool>
    {
        public int ProductId { get; init; }
        public short Quantity { get; init; }
    }
}
