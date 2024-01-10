using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class User : DomainEntity<Guid>
{
    public Guid roleId { get; set; }
    public int officeId { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public char gentle { get; set; }
    public bool status { get; set; }
    public string deviceId { get; set; }
    public string deviceVersion { get; set; }
    public string OS { get; set; }

    public virtual ICollection<RefreshToken> refreshTokens { get; set; }
    public virtual ICollection<Report> reports { get; set; }
    public virtual ICollection<UserPackage> userPackages { get; set; }

}
