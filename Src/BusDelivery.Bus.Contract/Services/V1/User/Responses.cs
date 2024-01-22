using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.User;
public static class Responses
{
    public record UserResponse(
        Guid RoleId,
        int OfficeId,
        string Name,
        string Email,
        string HashPassword,
        string PhoneNumber,
        Gentle Gentle,
        string? DeviceId,
        string? DeviceVersion,
        string? OS,
        DateTime CreateTime,
        bool IsDeleted,
        bool IsActive);
}
