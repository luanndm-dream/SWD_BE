using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Role : DomainEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<User> Users { get; set; }

    public void Update(string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
    }
}
