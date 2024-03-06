namespace BusDelivery.Contract.Services.V1.Order;
public class Responses
{
    public record OrderResponses(
        int Id,
        int PackageId,
        string image,
        float weight,
        float price,
        string note,
        string contact
        );
}
