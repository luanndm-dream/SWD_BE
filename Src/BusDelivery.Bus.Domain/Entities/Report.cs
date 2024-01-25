using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Report : DomainEntity<int>
{
    public string Content { get; set; }
    public int CreateBy { get; set; }
    public int TargetId { get; set; }
    public string Type { get; set; }
    public DateTime CreateTime { get; set; }
}
