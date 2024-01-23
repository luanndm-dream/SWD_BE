using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Authentication;
public static class Responses
{
    public record LoginResponses(
        Guid Id,
        Guid RoleId,
        int OfficeId,
        string Name,
        string Email,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        string DeviceId,
        string DeviceVersion,
        OS? OS,
        string CreateTime,
        bool IsActive,
        string Token,
        DateTime ExpireOn);
}
