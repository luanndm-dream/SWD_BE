namespace BusDelivery.Contract.Services.V1.Role;
public static class Responses
{
    public record RoleResponse(
        int Id,
        string Name,
        string Description);
}
