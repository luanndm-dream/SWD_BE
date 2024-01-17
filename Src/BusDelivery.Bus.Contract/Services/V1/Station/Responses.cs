
namespace BusDelivery.Contract.Services.V1.Station;
public static class Responses
{
    public record GetStationResponse(
        int id,
        int officeId,
        string name,
        string lat,
        string lng);



}
