namespace Common.Application.Abstraction
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
