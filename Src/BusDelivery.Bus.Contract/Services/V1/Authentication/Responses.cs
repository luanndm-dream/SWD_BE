using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Authentication;
public static class Responses
{
    public record LoginResponses(
        int Id,
        int RoleId,
        string RoleDescription,
        int OfficeId,
        string Name,
        string Email,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        string? DeviceId,
        string? DeviceVersion,
        OS? OS,
        string CreateTime,
        bool IsActive,
        string Token,
        string ExpireOn);
}
