namespace BusDelivery.Contract.Services.V1.Route;
public class Responses
{
    public class RouteResponse
    {
        public int id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
        public string StartPoint { get; set; } = null!;
        public string EndPoint { get; set; } = null!;
        public string OperateTime { get; set; } = null!;

        public List<StationResponse> Stations { get; set; }
    }

    public record StationResponse(
        int Id,
        int OfficeId,
        string Name,
        string Lat,
        string Lng,
        bool IsActive);
}
