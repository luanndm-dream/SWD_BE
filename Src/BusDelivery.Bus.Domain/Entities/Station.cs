using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Station : DomainEntity<int>
{
    public int OfficeId { get; set; }
    public string Name { get; set; }
    public string Lat { get; set; }
    public string Lng { get; set; }
    public string IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Package> Packages { get; set; }
    public virtual ICollection<StationRoute> StationRoutes { get; set; }
}
