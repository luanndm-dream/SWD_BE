using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Station;
using BusDelivery.Persistence;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Station.Commands;
public sealed class DeleteStationCommandHandler : ICommandHandler<Command.DeleteStationRequest>
{
    private readonly StationRepository stationRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public DeleteStationCommandHandler(StationRepository stationRepository, ApplicationDbContext context, IMapper mapper)
    {
        this.stationRepository = stationRepository;
        this.context = context; 
        this.mapper = mapper;   
    }

    public async Task<Result> Handle(Command.DeleteStationRequest request, CancellationToken cancellationToken)
    {
        var station = context.Stations.Where( x => x.id == request.stationID).SingleOrDefault()
            ?? throw new Exception("Station not found");
        try
        {
            stationRepository.Remove(station);
            context.SaveChanges();
            return Result.Success(202);
        }
        catch
        {
            throw new Exception("Delete not success");
        }

    }
}
