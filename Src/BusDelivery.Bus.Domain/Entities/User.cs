using BusDelivery.Contract.Enumerations;
using BusDelivery.Domain.Abstractions.EntityBase;
using static BusDelivery.Contract.Services.V1.User.Responses;

namespace BusDelivery.Domain.Entities;
public class User : DomainEntity<int>
{
    public int RoleId { get; set; }
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
    public DateTime CreateTime { get; set; }
    public bool IsActive { get; set; }
    public string? Avatar { get; set; }

    public virtual ICollection<Report> Reports { get; set; }

    public void Update(
        int Id,
        int RoleId,
        int OfficeId,
        string Name,
        string Email,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        string? DeviceId,
        string? DeviceVersion,
        OS? OS,
        bool IsActive,
        string? Avatar = "")
    {
        this.Id = Id;
        this.RoleId = RoleId;
        this.OfficeId = OfficeId;
        this.Name = Name;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.Identity = Identity;
        this.Gentle = Gentle;
        this.DeviceId = DeviceId;
        this.DeviceVersion = DeviceVersion;
        this.OS = OS;
        this.IsActive = IsActive;
        this.Avatar = Avatar;
    }

    public UserResponse ToResponses(string roleDescription)
        => new UserResponse
        {
            Id = Id,
            RoleId = RoleId,
            RoleDescription = roleDescription,
            OfficeId = OfficeId,
            Name = Name,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Identity = Identity,
            Gentle = Gentle,
            DeviceId = DeviceId,
            DeviceVersion = DeviceVersion,
            OS = OS,
            CreateTime = CreateTime.ToString("dd/MM/yyyy"),
            IsActive = IsActive,
            Avatar = Avatar
        };

}
