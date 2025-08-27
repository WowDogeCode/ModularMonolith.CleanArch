namespace Common.Application.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int entityId);
        Task<T?> GetByIdAsync(int entityId);
    }
}