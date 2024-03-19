using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class OfficeRepository : RepositoryBase<Office, int>
{
    private readonly ApplicationDbContext context;
    public OfficeRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }
    public void Delete(Office office)
    {
        office.IsActive = false;
        Update(office);
    }

    public int Count()
        => context.Office.Count();
}
