using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Role : DomainEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<User> Users { get; set; }
}
