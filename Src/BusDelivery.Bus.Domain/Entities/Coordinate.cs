using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Coordinate : DomainEntity<int>
{
    public int RouteId { get; set; }
    public string Lat { get; set; }
    public string Lng { get; set; }
    public int Stt { get; set; }
}
