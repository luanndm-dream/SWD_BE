using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Order : DomainEntity<Guid>
{
    public Guid packageId { get; set; }
    public string image { get; set; }
    public float weight { get; set; }
    public float price { get; set; }
    public string note { get; set; }
    public string contact { get; set; }
}
