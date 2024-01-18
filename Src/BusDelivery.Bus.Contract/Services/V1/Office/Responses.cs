namespace BusDelivery.Contract.Services.V1.Office;
public static class Responses
{
    public record OfficeReponses(
        int id,
        string name,
        string address,
        string lat,
        string lng,
        string contact,
        string image,
        bool IsActive);
}
