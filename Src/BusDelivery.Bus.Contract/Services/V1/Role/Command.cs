using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Role;
public class Command
{
    public record CreateRoleCommand(
        string Name,
        string Description) : ICommand<Responses.RoleResponse>;

    public record UpdateRoleCommand(
        Guid Id,
        string Name,
        string Description) : ICommand<Responses.RoleResponse>;

    public record DeleteRoleCommand(Guid Id) : ICommand;
}
