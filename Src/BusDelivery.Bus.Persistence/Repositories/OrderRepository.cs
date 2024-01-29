using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class OrderRepository : RepositoryBase<Order, int>
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }
}
