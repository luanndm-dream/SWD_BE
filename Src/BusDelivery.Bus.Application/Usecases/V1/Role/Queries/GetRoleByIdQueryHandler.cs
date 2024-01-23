using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Role;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Role.Queries;
public sealed class GetRoleByIdQueryHandler : IQueryHandler<Query.GetRoleByIdQuery, Responses.RoleResponse>
{
    private readonly RoleRepository roleRepository;
    private readonly IMapper mapper;

    public GetRoleByIdQueryHandler(RoleRepository roleRepository, IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RoleResponse>> Handle(Query.GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await roleRepository.FindByIdAsync(request.Id)
            ?? throw new RoleException.RoleIdNotFoundException(request.Id);

        var resultResponse = mapper.Map<Responses.RoleResponse>(result);
        return Result.Success(resultResponse);
    }
}
