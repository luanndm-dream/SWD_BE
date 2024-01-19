namespace BusDelivery.Contract.Services.V1.Coordinate;
public static class Responses
{
    public record CoordinateResponses(
    int id,
    string lat,
    string lng,
    int stt,
    int routeId,
    bool status);
}
