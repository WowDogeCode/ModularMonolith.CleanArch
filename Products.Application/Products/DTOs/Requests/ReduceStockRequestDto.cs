namespace Products.Application.Products.DTOs.Requests
{
    public sealed class ReduceStockRequestDto
    {
        public int ProductId { get; init; }
        public short Quantity { get; init; }
    }
}
