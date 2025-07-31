using Products.Application.Products.Dtos;

namespace Products.Application.Abstraction.Repositories
{
    public interface IProductReadRepository 
    {
        Task<List<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken);
    }
}
