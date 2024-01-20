using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Authentication;
public static class Responses
{
    public record UserReponses(
        Guid RoleId,
        int OfficeId,
        string Name,
        string Email,
        string HashPassword,
        string PhoneNumber,
        Gentle Gentle,
        string DeviceId,
        string DeviceVersion,
        string CreateTime,
        string OS,
        bool IsActive);

    public record LoginResponses(
        Guid Id,
        Guid RoleId,
        int OfficeId,
        string Name,
        string Email,
        string PhoneNumber,
        Gentle Gentle,
        string DeviceId,
        string DeviceVersion,
        string CreateTime,
        string OS,
        bool IsActive,
        string Token,
        DateTime ExpireOn);
}
