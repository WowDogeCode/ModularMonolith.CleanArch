namespace Common.Application.Abstraction
{
    public interface IInventoryService
    {
        Task ReduceStockAsync(int productId, short quantity, CancellationToken cancellationToken);
    }
}
