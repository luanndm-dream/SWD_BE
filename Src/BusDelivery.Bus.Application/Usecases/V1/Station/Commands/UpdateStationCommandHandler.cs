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
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Station.Commands;
public sealed class UpdateStationCommandHandler : ICommandHandler<Command.UpdateStationRequest, Responses.GetStationResponse>
{
    private readonly IMapper mapper;
    private readonly ApplicationDbContext context;
    private readonly StationRepository stationRepository;
    public UpdateStationCommandHandler(IMapper mapper, ApplicationDbContext dbContext, StationRepository stationRepository)
    {
        this.mapper = mapper;
        this.stationRepository = stationRepository;
        context = dbContext;   
    }
    public async Task<Result<Responses.GetStationResponse>> Handle(Command.UpdateStationRequest request, CancellationToken cancellationToken)
    {
        var station = await context.Stations.Where(x => x.id == request.stationId).SingleOrDefaultAsync(cancellationToken);

        if (station == null)
        {
            throw new Exception("Station not found");
        }
        else
        {
            station.officeId = request.officeId;
            station.name = request.name;
            station.lat = request.lat;
            station.lng = request.lng;
            stationRepository.Update(station);
            context.SaveChanges();
            var response = mapper.Map<Responses.GetStationResponse>(station);
            return await Task.FromResult(response);

        }
        
        
    }
}
