using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Products.Application.Abstraction.Repositories;
using Products.Domain.Entities;

namespace Products.Infrastructure.Repositories
{
    public sealed class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductByName(string productName, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productName).ConfigureAwait(false);

            return product;
        }
        public async Task<bool> TryDecrementStockAsync(int productId, short quantity, CancellationToken cancellationToken)
        {
            var affected = await _context.Products.Where(p => p.Id == productId && p.UnitsInStock + p.UnitsOnOrder >= quantity)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.UnitsInStock, x => x.UnitsInStock - quantity), cancellationToken)
                .ConfigureAwait(false);

            return affected > 0;
        }
    }
}