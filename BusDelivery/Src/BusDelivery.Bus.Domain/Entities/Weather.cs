using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Weather : DomainEntity<int>
{
    public int officeId { get; set; }
    public double temperature { get; set; }
    public double humidity { get; set; }
    public double windySpeed { get; set; }
    public string recordAt { get; set; }
}
