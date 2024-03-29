﻿using System;
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
public class UpdateRouteCommandHandler : ICommandHandler<Command.UpdateRouteCommandRequest, Responses.RouteResponse>
{
    private readonly RouteRepository routeRepository;
    private readonly IMapper mapper;

    public UpdateRouteCommandHandler(RouteRepository routeRepository, IMapper mapper)
    {
        this.routeRepository = routeRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RouteResponse>> Handle(Command.UpdateRouteCommandRequest request, CancellationToken cancellationToken)
    {
        var route = await routeRepository.FindByIdAsync(request.id)
            ?? throw new RouteException.RouteIdNotFoundException(request.id);
        try
        {
            route.Name = request.Name;
            route.Description = request.Description;
            route.IsActive = request.IsActive;
            route.StartPoint = request.StartPoint;
            route.EndPoint = request.EndPoint;
            route.OperateTime = request.OperateTime;
        }catch
        {
            throw new Exception("Update route error!");

        }
        routeRepository.Update(route);
        var response = mapper.Map<Responses.RouteResponse>(route);
        return Result.Success(response); 
    }
}
