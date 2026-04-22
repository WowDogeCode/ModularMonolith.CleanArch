using Common.Application.DTOs;
using Products.Application.Products.DTOs;

namespace Products.Application.Abstraction.Repositories
{
    public interface IProductReadRepository 
    {
        Task<List<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<List<ProductInventorySnapshotDto>> GetProductInventorySnapshotsAsync(List<int> productIds, CancellationToken cancellationToken);
    }
}
