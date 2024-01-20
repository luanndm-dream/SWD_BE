namespace BusDelivery.Contract.Services.V1.Office;
public static class Responses
{
    public record OfficeResponse(
        int Id,
        string Name,
        string Address,
        string Lat,
        string Lng,
        string Contact,
        string Image,
        bool IsActive);
}
