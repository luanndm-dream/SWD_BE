using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace BusDelivery.Contract.Services.V1.User;
public static class Command
{
    public record CreateUserCommand(
        int RoleId,
        int OfficeId,
        string Name,
        string Email,
        string Password,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        IFormFile Avatar) : ICommand<Responses.UserResponse>;

    public record UpdateUserCommand(
        int? Id,
        int RoleId,
        int OfficeId,
        string Name,
        string Email,
        string PhoneNumber,
        string Identity,
        Gentle Gentle,
        string? DeviceId,
        string? DeviceVersion,
        OS? OS,
        bool IsActive,
        IFormFile Avatar) : ICommand<Responses.UserResponse>;

    public record DeleteUserCommand(int Id) : ICommand;
}
