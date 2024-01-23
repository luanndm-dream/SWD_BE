using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Role;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Role.Commands;
public class UpdateRoleCommandHandler : ICommandHandler<Command.UpdateRoleCommand, Responses.RoleResponse>
{
    private readonly RoleRepository roleRepository;
    private readonly IMapper mapper;

    public UpdateRoleCommandHandler(RoleRepository roleRepository, IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.RoleResponse>> Handle(Command.UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new RoleException.RoleIdNotFoundException(request.Id);

        try
        {
            role.Update(request.Name, request.Description);
            roleRepository.Update(role);

            var resultResponse = mapper.Map<Responses.RoleResponse>(role);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
