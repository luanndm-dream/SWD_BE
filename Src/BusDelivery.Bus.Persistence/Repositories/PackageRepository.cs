using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class PackageRepository : RepositoryBase<Package, int>
{
    public PackageRepository(ApplicationDbContext context) : base(context)
    {
    }
}
