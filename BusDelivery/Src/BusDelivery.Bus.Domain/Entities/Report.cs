using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Report : DomainEntity<int>
{
    public string content { get; set; }
    public DateTime createTime { get; set; }
    public Guid createBy { get; set; }
    public int targetId { get; set; }
}
