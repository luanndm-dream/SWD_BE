using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Bus : DomainEntity<int>
{
    public string number { get; set; }
    public string plateNumber { get; set; }
    public string name { get; set; }
    public string organization { get; set; }
    public string color { get; set; }
    public string numberOfSeat { get; set; }
    public string operateTime { get; set; }
    public bool status { get; set; }

    public virtual ICollection<Route> routes { get; set; }
    public virtual ICollection<BusRoute> busRoutes { get; set; }
}
