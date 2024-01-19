using BusDelivery.Contract.Enumerations;
using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Package : DomainEntity<Guid>
{
    public int BusId { get; set; }
    public int OfficeId { get; set; }
    public int StationId { get; set; }
    public int Quantity { get; set; }
    public float TotalWeight { get; set; }
    public float TotalPrice { get; set; }
    public int Image { get; set; }
    public string Note { get; set; }
    public PackageStatus Status { get; set; }
    public DateTime CreateTime { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
