namespace Infrastructure.Persistence.Repositories
{
    public interface IGenericRepository<T> where T : class, new()
    {
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int entityId);
    }
}