using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.User;
public static class Responses
{
    public record UserResponse(
        Guid Id,
        Guid RoleId,
        int OfficeId,
        string Name,
        string Email,
        string HashPassword,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        string? DeviceId,
        string? DeviceVersion,
        OS? OS,
        string CreateTime,
        bool IsActive);
}
