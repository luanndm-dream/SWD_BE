namespace BusDelivery.Contract.Services.V1.Coordinate;
public static class Responses
{
    public record CoordinateResponses(
    int Id,
    int RouteId,
    string Lat,
    string Lng,
    int Stt
    );
}
