using Products.Application.Abstraction.Repositories;
using Products.Domain.Entities;

namespace Products.Infrastructure.Repositories
{
    sealed class ProductRepository : IProductRepository
    {
        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
