using Common.Application.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }
        public async Task DeleteAsync(int entityId)
        {
            T? entityToDelete = await _context.FindAsync<T>(entityId);

            if (entityToDelete != null)
            {
                _context.Remove(entityToDelete);
            }
        }
        public async Task<T?> GetByIdAsync(int entityId)
        {
            T? entity = await _context.Set<T>().FindAsync(entityId);

            return entity;
        }
        public async Task<T?> GetByIdAsNoTrackingAsync(int entityId, CancellationToken cancellationToken)
        {
            T? entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == entityId, cancellationToken).ConfigureAwait(false);

            return entity;
        }
        public Task UpdateAsync(T entity)
        {
            _context.Update(entity);

            return Task.CompletedTask;
        }
    }
}
