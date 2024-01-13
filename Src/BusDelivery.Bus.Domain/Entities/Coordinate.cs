using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Coordinate : DomainEntity<int>
{
    public double lat { get; set; }
    public double lng { get; set; }
    public int stt { get; set; }
    public int routeId { get; set; }
}
