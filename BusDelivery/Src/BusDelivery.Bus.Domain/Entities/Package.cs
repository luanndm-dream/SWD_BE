using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Package : DomainEntity<Guid>
{
    public Guid userId { get; set; }
    public int quantity { get; set; }
    public int officeId { get; set; }
    public float weight { get; set; }
    public float price { get; set; }
    public int image { get; set; }
    public string note { get; set; }
    public string status { get; set; }
    public DateTime createTime { get; set; }

    public virtual ICollection<UserPackage> userPackages { get; set; }
    public virtual ICollection<OfficePackage> officePackages { get; set; }
}
