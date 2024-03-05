using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Route;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Route.Queries;
public class GetRouteByIdHandler : IQueryHandler<Query.GetRouteById, Responses.RouteResponse>
{
    private readonly RouteRepository routeRepository;
    private readonly IMapper mapper;

    public GetRouteByIdHandler(
        RouteRepository routeRepository,
        IMapper mapper)
    {
        this.routeRepository = routeRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RouteResponse>> Handle(Query.GetRouteById request, CancellationToken cancellationToken)
    {
        var route = await routeRepository.FindByIdAsync(request.routeId)
            ?? throw new RouteException.RouteIdNotFoundException(request.routeId);
        var response = mapper.Map<Responses.RouteResponse>(route);

        // Get Station by routeId
        var stations = await routeRepository.GetStationByRouteId(request.routeId);

        if (stations != null)
        {
            var stationsResponses = mapper.Map<List<Responses.StationResponse>>(stations);

            response.Stations = new List<Responses.StationResponse>();
            response.Stations.AddRange(stationsResponses);
        }

        return Result.Success(response);
    }
}
