using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Station;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Station.Queries;
public sealed class GetStationHandler : IQueryHandler<Query.GetStation, PagedResult<Responses.GetStationResponse>>
{
    private readonly StationRepository stationRepository;
    private readonly IMapper mapper;

    public GetStationHandler(StationRepository stationRepository, IMapper mapper)
    {
        this.stationRepository = stationRepository;
        this.mapper = mapper;
    }
    public async Task<Result<PagedResult<Responses.GetStationResponse>>> Handle(Query.GetStation request, CancellationToken cancellationToken)
    {
        var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
          ? stationRepository.FindAll()
          : stationRepository.FindAll(x => x.Name.Contains(request.searchTerm));

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);
            ;

        var Events = await PagedResult<Domain.Entities.Station>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        var result = mapper.Map<PagedResult<Responses.GetStationResponse>>(Events);
        return Result.Success(result);
    }

    // return e => e.property
    public static Expression<Func<Domain.Entities.Station, object>> GetSortProperty(Query.GetStation request)
        => request.sortColumn?.ToLower() switch
        {
            "name" => e => e.Name,
            //"address" => e => e.address,
            _ => e => e.Name
        };
}

