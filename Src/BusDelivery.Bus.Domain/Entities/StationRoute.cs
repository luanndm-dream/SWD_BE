namespace BusDelivery.Domain.Entities;
public class StationRoute
{
    public int StationId { get; set; }
    public int RouteId { get; set; }

    public virtual Station Station { get; set; }
    public virtual Route Route { get; set; }
}
