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

namespace BusDelivery.Application.Usecases.V1.Route.Commands;
public class DeleteRouteCommandHandler : ICommandHandler<Command.DeleteRouteCommandRequest>
{
    private readonly RouteRepository routeRepository;
    private readonly IMapper mapper;

    public DeleteRouteCommandHandler(RouteRepository routeRepository, IMapper mapper)
    {
        this.routeRepository = routeRepository;
        this.mapper = mapper;
    }

    public async Task<Result> Handle(Command.DeleteRouteCommandRequest request, CancellationToken cancellationToken)
    {
        var route = await routeRepository.FindByIdAsync(request.id)
            ?? throw new RouteException.RouteIdNotFoundException(request.id);
        try
        {
            routeRepository.Remove(route);
            return Result.Success(202);
        }
        catch
        {
            throw new Exception("Delete route not success!");
        }
    }
}
