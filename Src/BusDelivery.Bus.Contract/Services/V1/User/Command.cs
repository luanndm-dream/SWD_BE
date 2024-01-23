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
        string Password,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        bool IsActive) : ICommand<Responses.UserResponse>;

    public record UpdateUserCommand(
        Guid Id,
        Guid RoleId,
        int OfficeId,
        string Name,
        string Email,
        string Password,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        string? DeviceId,
        string? DeviceVersion,
        OS? OS,
        string CreateTime,
        bool IsActive) : ICommand<Responses.UserResponse>;

    public record DeleteUserCommand(Guid Id) : ICommand;
}
