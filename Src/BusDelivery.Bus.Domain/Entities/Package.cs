using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Package : DomainEntity<Guid>
{
    public Guid userId { get; set; }
    public int officeId { get; set; }
    public int busId { get; set; }
    public int stationId { get; set; }
    public int quantity { get; set; }
    public float totalWeight { get; set; }
    public float totalPrice { get; set; }
    public int image { get; set; }
    public string note { get; set; }
    public string status { get; set; }
    public DateTime createTime { get; set; }

    public virtual ICollection<UserPackage> userPackages { get; set; }
    public virtual ICollection<Order> orders { get; set; }
}
