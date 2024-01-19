using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Coordinate;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Coordinate.Queries;
public sealed class GetCoordinateQueryHandler : IQueryHandler<Query.GetCoordinateQuery, PagedResult<Responses.CoordinateResponses>>
{
    private readonly CoordinateRepository coordinateRepository;
    private readonly IMapper mapper;

    public GetCoordinateQueryHandler(CoordinateRepository coordinateRepository, IMapper mapper)
    {
        this.coordinateRepository = coordinateRepository;
        this.mapper = mapper;
    }
    public async Task<Result<PagedResult<Responses.CoordinateResponses>>> Handle(Query.GetCoordinateQuery request, CancellationToken cancellationToken)
    {
        var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
        ? coordinateRepository.FindAll()
        : coordinateRepository.FindAll(x => x.Stt.ToString().Contains(request.searchTerm));

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
        ? EventsQuery.OrderByDescending(keySelector)
        : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.Coordinate>.CreateAsync(EventsQuery,
        request.pageIndex,
        request.pageSize);

        var result = mapper.Map<PagedResult<Responses.CoordinateResponses>>(Events);
        return Result.Success(result);
    }
    public static Expression<Func<Domain.Entities.Coordinate, object>> GetSortProperty(Query.GetCoordinateQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "stt" => e => e.Stt,
    };
}
