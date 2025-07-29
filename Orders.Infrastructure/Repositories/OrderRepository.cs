using Common.Infrastructure;
using Orders.Application.Abstraction.Repositories;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.Repositories
{
    public sealed class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
