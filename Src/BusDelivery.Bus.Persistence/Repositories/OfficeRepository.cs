using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class OfficeRepository : RepositoryBase<Office, int>
{
    public OfficeRepository(ApplicationDbContext context) : base(context)
    {

    }
    public void Delete(Office office)
    {
        office.IsActive = false;
        Update(office);
    }
}
