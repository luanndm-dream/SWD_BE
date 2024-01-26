using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class PackageRepository : RepositoryBase<Package, Guid>
{
    public PackageRepository(ApplicationDbContext context) : base(context)
    {
    }
}
