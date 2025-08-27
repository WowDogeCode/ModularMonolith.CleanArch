using Common.Application.Abstraction;
using System.Data;

namespace Common.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbTransaction? _transaction;
        public UnitOfWork(ApplicationDbContext context, IDbTransaction? transaction = null)
        {
            _context = context;
            _transaction = transaction;
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _transaction?.Commit();
        }
    }
}
