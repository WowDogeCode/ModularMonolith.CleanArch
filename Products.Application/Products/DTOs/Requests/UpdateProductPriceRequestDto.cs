namespace Products.Application.Products.DTOs.Requests
{
    public sealed class UpdateProductPriceRequestDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
