using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Role;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Role.Queries;
public sealed class GetRoleQueryHandler : IQueryHandler<Query.GetRoleQuery, IReadOnlyCollection<Responses.RoleResponse>>
{
    private readonly RoleRepository roleRepository;
    private readonly IMapper mapper;

    public GetRoleQueryHandler(RoleRepository roleRepository, IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<Result<IReadOnlyCollection<Responses.RoleResponse>>> Handle(Query.GetRoleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = roleRepository.FindAll();

            var resultResponses = mapper.Map<IReadOnlyCollection<Responses.RoleResponse>>(result);

            return Result.Success(resultResponses);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
