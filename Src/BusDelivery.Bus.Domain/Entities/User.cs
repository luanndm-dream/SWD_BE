using BusDelivery.Contract.Enumerations;
using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class User : DomainEntity<Guid>
{
    public Guid RoleId { get; set; }
    public int OfficeId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
    public string PhoneNumber { get; set; }
    public string Identity { get; set; }
    public Gentle Gentle { get; set; }
    public string? DeviceId { get; set; }
    public string? DeviceVersion { get; set; }
    public OS? OS { get; set; }
    public string CreateTime { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Report> Reports { get; set; }

    public void Update(
        Guid Id,
        Guid RoleId,
        int OfficeId,
        string Name,
        string Email,
        string HashPassword,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        string? DeviceId,
        string? DeviceVersion,
        OS? OS,
        string CreateTime,
        bool IsActive)
    {
        this.Id = Id;
        this.RoleId = RoleId;
        this.OfficeId = OfficeId;
        this.Name = Name;
        this.Email = Email;
        this.HashPassword = HashPassword;
        this.PhoneNumber = PhoneNumber;
        this.Identity = Identity;
        this.Gentle = Gentle;
        this.DeviceId = DeviceId;
        this.DeviceVersion = DeviceVersion;
        this.OS = OS;
        this.CreateTime = CreateTime;
        this.IsActive = IsActive;
    }
}
