using Common.Application.Abstraction;
using Products.Domain.Entities;

namespace Products.Application.Abstraction.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
