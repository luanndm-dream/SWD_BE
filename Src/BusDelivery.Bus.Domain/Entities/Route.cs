using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Route : DomainEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }
    public string StartPoint { get; set; }
    public string EndPoint { get; set; }
    public string OperateTime { get; set; }

    public virtual ICollection<Coordinate> Coordinates { get; set; }
    public virtual ICollection<BusRoute> BusRoutes { get; set; }
    public virtual ICollection<StationRoute> StationRoutes { get; set; }

}
