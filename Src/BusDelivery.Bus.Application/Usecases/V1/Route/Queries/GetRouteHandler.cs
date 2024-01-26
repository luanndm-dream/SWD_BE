using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Route;
using BusDelivery.Persistence.Repositories;
using MediatR;

namespace BusDelivery.Application.Usecases.V1.Route.Queries;
public class GetRouteHandler : IQueryHandler<Query.GetRoute,PagedResult< Responses.RouteResponse>>
{
    private readonly RouteRepository routeRepository;
    private readonly IMapper mapper;

    public GetRouteHandler(RouteRepository routeRepository, IMapper mapper)
    {
        this.routeRepository = routeRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.RouteResponse>>> Handle(Query.GetRoute request, CancellationToken cancellationToken)
    {
        var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
        ? routeRepository.FindAll()
        : routeRepository.FindAll(x => x.Name.Contains(request.searchTerm));

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.Route>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        var result = mapper.Map<PagedResult<Responses.RouteResponse>>(Events);
        return Result.Success(result);
    }
    public static Expression<Func<Domain.Entities.Route, object>> GetSortProperty(Query.GetRoute request)
       => request.sortColumn?.ToLower() switch
       {
           "name" => e => e.Name,
           _ => e => e.Name
       };
}

