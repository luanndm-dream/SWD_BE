using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class User : DomainEntity<Guid>
{
    public Guid RoleId { get; set; }
    public int OfficeId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public char Gentle { get; set; }
    public bool Status { get; set; }
    public string DeviceId { get; set; }
    public string DeviceVersion { get; set; }
    public string OS { get; set; }
    public virtual ICollection<Report> Reports { get; set; }
}
