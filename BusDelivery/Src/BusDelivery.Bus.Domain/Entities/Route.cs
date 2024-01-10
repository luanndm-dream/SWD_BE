using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Route : DomainEntity<int>
{
    public string name { get; set; }
    public string description { get; set; }
    public int officeId { get; set; }
    public int busId { get; set; }
    public bool status { get; set; }
    public string startPoint { get; set; }
    public string endPoint { get; set; }
    public string operateTime { get; set; }

    public virtual ICollection<Path> paths { get; set; }
    public virtual ICollection<BusRoute> busRoutes { get; set; }
}
