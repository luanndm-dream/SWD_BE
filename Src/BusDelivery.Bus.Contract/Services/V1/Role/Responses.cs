namespace BusDelivery.Contract.Services.V1.Role;
public class Responses
{
    public record RoleResponse(
        Guid Id,
        string Name,
        string Description);
}
