using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;

public class RefreshToken : DomainEntity<Guid>
{
    public Guid userId { get; set; }
    public string clientId { get; set; }
    public string deviceId { get; set; }
    public string token { get; set; }
    public DateTime expiresOn { get; set; }
    public bool isExpired => DateTime.Now >= expiresOn;
    public DateTime createdOn { get; set; } = DateTime.Now;
}
