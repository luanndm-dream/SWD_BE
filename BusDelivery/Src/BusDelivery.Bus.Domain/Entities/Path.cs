using BusDelivery.Domain.Abstractions.EntityBase;

namespace BusDelivery.Domain.Entities;
public class Path : DomainEntity<int>
{
    public double lt { get; set; }
    public double ln { get; set; }
    public int stt { get; set; }
    public int routeId { get; set; }
}
