namespace Products.Application.Products.DTOs.Requests
{
    public sealed class UpdateProductStockRequestDto
    {
        public int ProductId { get; set; }
        public short Stock { get; set; }
    }
}
