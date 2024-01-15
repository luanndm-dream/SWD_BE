
namespace BusDelivery.Contract.Services.V1.Station;
public class Responses
{
    public record GetStationResponse(
        int id,
        int officeId,
        string name,
        string lat,
        string lng,
        bool status);



}
