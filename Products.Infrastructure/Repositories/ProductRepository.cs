using Common.Infrastructure;
using Products.Application.Abstraction.Repositories;
using Products.Domain.Entities;

namespace Products.Infrastructure.Repositories
{
    public sealed class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
