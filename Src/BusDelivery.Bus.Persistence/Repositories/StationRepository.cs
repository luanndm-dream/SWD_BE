using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class StationRepository : RepositoryBase<Station, int>
{
    public StationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
