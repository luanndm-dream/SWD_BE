using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Queries;
public sealed class GetUserQueryHandler : IQueryHandler<Query.GetUserQuery, PagedResult<Responses.UserResponse>>
{
    private readonly UserRepository userRepository;
    private readonly IMapper mapper;
    public GetUserQueryHandler(UserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }
    public async Task<Result<PagedResult<Responses.UserResponse>>> Handle(Query.GetUserQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.User> EventsQuery;

        EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? (request.roleId == null
            ? userRepository.FindAll()
            : userRepository.FindAll(x => x.RoleId == request.roleId))
            : userRepository.FindAll(x => x.Name.Contains(request.searchTerm) && (request.roleId == null || x.RoleId == request.roleId));

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.User>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        var result = mapper.Map<PagedResult<Responses.UserResponse>>(Events);
        return Result.Success(result);
    }

    public static Expression<Func<Domain.Entities.User, object>> GetSortProperty(Query.GetUserQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "Name" => e => e.Name,
        _ => e => DateTime.ParseExact(e.CreateTime, "dd/MM/yyyy", null)
    };
}
