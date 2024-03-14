using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Queries;
public sealed class GetUserQueryHandler : IQueryHandler<Query.GetUserQuery, PagedResult<Responses.UserResponse>>
{
    private readonly UserRepository userRepository;
    private readonly RoleRepository roleRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageRepository blobStorageRepository;
    public GetUserQueryHandler(
        UserRepository userRepository,
        IMapper mapper,
        RoleRepository roleRepository,
        IBlobStorageRepository blobStorageRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.roleRepository = roleRepository;
        this.blobStorageRepository = blobStorageRepository;
    }
    public async Task<Result<PagedResult<Responses.UserResponse>>> Handle(Query.GetUserQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.User> EventsQuery;

        EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? (request.officeId == null
            ? userRepository.FindAll()
            : userRepository.FindAll(x => x.OfficeId == request.officeId))
            : userRepository.FindAll(x => x.Name.Contains(request.searchTerm) && (request.officeId == null || x.OfficeId == request.officeId));

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.User>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);


        var result = mapper.Map<PagedResult<Responses.UserResponse>>(Events);
        foreach (var user in result.items)
        {
            user.RoleDescription = roleRepository.FindByIdAsync(user.RoleId).GetAwaiter().GetResult().Description;
            user.Avatar = await blobStorageRepository.GetImageToBase64(user.Avatar);
        }

        // Encode toBase64String
        foreach (var user in Events.items)
        {
            user.Avatar = await blobStorageRepository.GetImageToBase64(user.Avatar);
        }
        return Result.Success(result);
    }

    private static Expression<Func<Domain.Entities.User, object>> GetSortProperty(Query.GetUserQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "name" => e => e.Name,
        _ => e => e.CreateTime
    };
}
