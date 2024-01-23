using BusDelivery.Contract.Enumerations;
using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Package : DomainEntity<Guid>
{
    public int BusId { get; set; }
    public int FromOfficeId { get; set; }
    public int ToOfficeId { get; set; }
    public int StationId { get; set; }
    public int Quantity { get; set; }
    public float TotalWeight { get; set; }
    public float TotalPrice { get; set; }
    public string Image { get; set; }
    public string Note { get; set; }
    public PackageStatus Status { get; set; }
    public string CreateTime { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
