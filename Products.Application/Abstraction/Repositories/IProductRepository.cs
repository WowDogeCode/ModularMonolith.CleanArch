using Products.Domain.Entities;

namespace Products.Application.Abstraction.Repositories
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
    }
}
