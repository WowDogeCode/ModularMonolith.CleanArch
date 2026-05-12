using Common.Application.DTOs;

namespace Common.Application.Abstraction
{
    public interface IInventoryService
    {
        Task<List<ProductInventorySnapshotDto>> GetProductInventorySnapshotsAsync(List<int> productIds, CancellationToken cancellationToken);
        Task<bool> IncreaseStockAsync(int productId, short quantity, CancellationToken cancellationToken);
        Task<bool> ReduceStockAsync(int productId, short quantity, CancellationToken cancellationToken);
    }
}
