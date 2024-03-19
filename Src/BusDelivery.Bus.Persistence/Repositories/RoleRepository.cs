using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class RoleRepository : RepositoryBase<Role, int>
{
    private readonly ApplicationDbContext context;
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsAdmin(int roleId)
    {
        var role = await context.Role.FindAsync(roleId);
        return role.Name.ToLower().Equals("admin");
    }
}
