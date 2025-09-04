using Products.Application.Products.DTOs;

namespace Products.Application.Abstraction.Repositories
{
    public interface IProductReadRepository 
    {
        Task<List<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken);
    }
}
