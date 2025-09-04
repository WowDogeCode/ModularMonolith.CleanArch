using MediatR;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
