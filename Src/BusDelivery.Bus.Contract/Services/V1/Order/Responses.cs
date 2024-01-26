namespace BusDelivery.Contract.Services.V1.Order;
public class Responses
{
    public record OrderResponses(
        Guid Id,
        Guid PackageId,
        string image,
        float weight,
        float price,
        string note,
        string contact
        );
}
