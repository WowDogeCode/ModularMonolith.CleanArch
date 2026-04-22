using Common.Application.DTOs;

namespace Common.Application.Abstraction
{
    public interface IInventoryService
    {
        Task<bool> ReduceStockAsync(int productId, short quantity, CancellationToken cancellationToken);
        Task<List<ProductInventorySnapshotDto>> GetProductInventorySnapshotsAsync(List<int> productIds, CancellationToken cancellationToken);
    }
}
