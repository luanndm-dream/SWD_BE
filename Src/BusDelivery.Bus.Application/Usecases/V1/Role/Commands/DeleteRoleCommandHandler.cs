using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Role;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Role.Commands;
public sealed class DeleteRoleCommandHandler : ICommandHandler<Command.DeleteRoleCommand>
{
    private readonly RoleRepository roleRepository;
    public DeleteRoleCommandHandler(RoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public async Task<Result> Handle(Command.DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var roleExist = await roleRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new RoleException.RoleIdNotFoundException(request.Id);

        try
        {
            roleRepository.Remove(roleExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
