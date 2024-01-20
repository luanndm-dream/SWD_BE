using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Role;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Role.Commands;
public sealed class CreateRoleCommandHandler : ICommandHandler<Command.CreateRoleCommand, Responses.RoleResponse>
{
    private readonly RoleRepository roleRepository;
    private readonly IMapper mapper;

    public CreateRoleCommandHandler(RoleRepository roleRepository, IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RoleResponse>> Handle(Command.CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var RoleNameExist = roleRepository.FindAll(x => x.Name == request.Name)
            ?? throw new RoleException.RoleBadRequestException("RoleName is already exist");

        var role = new Domain.Entities.Role
        {
            Name = request.Name,
            Description = request.Description,
        };

        try
        {
            roleRepository.Add(role);
            var resultResponse = mapper.Map<Responses.RoleResponse>(role);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
