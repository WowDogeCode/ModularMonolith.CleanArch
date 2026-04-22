using Common.Application.Abstraction;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(ApplicationDbContext context, IDbContextTransaction? transaction = null)
        {
            _context = context;
            _transaction = null;
        }

        // Use only for transactional operations
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null)
            {
                _transaction = await _context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (_transaction is not null)
            {
                await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
                await _transaction.DisposeAsync().ConfigureAwait(false);
                _transaction = null;
            }
        }
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                await _transaction.DisposeAsync().ConfigureAwait(false);
                _transaction = null;
            }
        }
    }
}
