using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Route;
using BusDelivery.Persistence;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Route.Commands;
public class CreateRouteCommandHandler : ICommandHandler<Command.CreateRouteCommandRequest, Responses.RouteResponse>
{
    //private readonly RouteRepository routeRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;


    public CreateRouteCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        //this.routeRepository = routeRepository;
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RouteResponse>> Handle(Command.CreateRouteCommandRequest request, CancellationToken cancellationToken)
    {
        var newRoute = new Domain.Entities.Route()
        {
            Name = request.Name,
            Description = request.Description,
            StartPoint = request.StartPoint,
            EndPoint = request.EndPoint,
            Status = request.Status,
            OperateTime = request.OperateTime

        };
        await context.AddAsync(newRoute);
        context.SaveChanges();
        var response = mapper.Map<Responses.RouteResponse>(newRoute);
        return Result.Success(response,201);
        
    }
}
