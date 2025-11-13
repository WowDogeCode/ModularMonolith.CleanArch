using MediatR;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.UpdateProductStock
{
    public record UpdateProductStockCommand : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public short Stock { get; set; }
    }
}
