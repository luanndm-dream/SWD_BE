using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Weather : DomainEntity<int>
{
    public int OfficeId { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double WindySpeed { get; set; }
    public string RecordAt { get; set; }
}
