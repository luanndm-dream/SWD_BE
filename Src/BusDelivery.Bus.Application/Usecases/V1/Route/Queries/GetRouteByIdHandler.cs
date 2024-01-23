using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public GetRouteByIdHandler(RouteRepository routeRepository, IMapper mapper)
    {
        this.routeRepository = routeRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RouteResponse>> Handle(Query.GetRouteById request, CancellationToken cancellationToken)
    {
        var route = await routeRepository.FindByIdAsync(request.routeId) 
            ?? throw new RouteException.RouteIdNotFoundException(request.routeId);

        var response = mapper.Map<Responses.RouteResponse>(route);
        return Result.Success(response);
    }
}
