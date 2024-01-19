using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Bus : DomainEntity<int>
{
    public string Number { get; set; }
    public string PlateNumber { get; set; }
    public string Name { get; set; }
    public string Organization { get; set; }
    public string Color { get; set; }
    public string NumberOfSeat { get; set; }
    public string OperateTime { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Package> Packages { get; set; }
    public virtual ICollection<BusRoute> BusRoutes { get; set; }
}
