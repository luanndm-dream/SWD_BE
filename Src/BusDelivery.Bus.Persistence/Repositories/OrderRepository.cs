using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class OrderRepository : RepositoryBase<Order, Guid>
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }
}
