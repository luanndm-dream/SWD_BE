using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.User;
public static class Command
{
    public record CreateUserCommand(
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
        bool IsActive) : ICommand<Responses.UserResponse>;

    public record UpdateUserCommand(
        Guid Id,
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

    public record DeleteUserCommand(Guid Id) : ICommand;
}
