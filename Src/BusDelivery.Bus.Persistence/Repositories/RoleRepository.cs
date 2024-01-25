using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class RoleRepository : RepositoryBase<Role, int>
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
