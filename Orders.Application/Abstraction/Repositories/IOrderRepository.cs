using Common.Application.Abstraction;
using Orders.Domain.Entities;

namespace Orders.Application.Abstraction.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
    }
}
