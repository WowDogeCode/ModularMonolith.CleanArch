using MediatR;
using Products.Application.Products.Dtos;

namespace Products.Application.Products.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
