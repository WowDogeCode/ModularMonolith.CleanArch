namespace Common.Application.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int entityId);
    }
}