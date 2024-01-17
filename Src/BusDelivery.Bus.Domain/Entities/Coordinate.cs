using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Coordinate : DomainEntity<int>
{
    public double Lat { get; set; }
    public double Lng { get; set; }
    public int Stt { get; set; }
    public int RouteId { get; set; }
}
