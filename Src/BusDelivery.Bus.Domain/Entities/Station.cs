using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Station : DomainEntity<int>
{
    public int officeId { get; set; }
    public string name { get; set; }
    public string lat { get; set; }
    public string lng { get; set; }
    public virtual ICollection<Package> packages { get; set; }
    public virtual ICollection<StationRoute> stationRoutes { get; set; }
}
