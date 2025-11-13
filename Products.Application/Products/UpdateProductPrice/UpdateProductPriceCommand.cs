using MediatR;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.UpdateProductPrice
{
    public record UpdateProductPriceCommand : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
