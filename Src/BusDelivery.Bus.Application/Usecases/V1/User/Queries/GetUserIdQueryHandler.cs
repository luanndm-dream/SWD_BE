using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Queries;
public sealed class GetUserIdQueryHandler : IQueryHandler<Query.GetUserByIdQuery, Responses.UserResponse>
{
    private readonly UserRepository userRepository;
    private readonly RoleRepository roleRepository;
    private readonly IMapper mapper;
    public GetUserIdQueryHandler(
        UserRepository userRepository,
        IMapper mapper,
        RoleRepository roleRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.roleRepository = roleRepository;
    }

    public async Task<Result<Responses.UserResponse>> Handle(Query.GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userExist = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserException.UserIdNotFoundException(request.Id);
        var role = await roleRepository.FindByIdAsync(userExist.RoleId);

        var resultResponse = userExist.ToResponses(role.Description);
        return Result.Success(resultResponse);
    }
}
