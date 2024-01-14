using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Role : DomainEntity<Guid>
{
    public string name { get; set; }
    public string description { get; set; }

    public virtual ICollection<User> users { get; set; }
}
