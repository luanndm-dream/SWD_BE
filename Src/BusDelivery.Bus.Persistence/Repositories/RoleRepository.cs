using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class RoleRepository : RepositoryBase<Role, Guid>
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
