using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Station;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;
using BusDelivery.Persistence;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Station.Commands;
public sealed class CreateStationCommandHandler : ICommandHandler<Command.CreateStationRequest, Responses.GetStationResponse>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly IMapper mapper;
    private readonly ApplicationDbContext context;
    private readonly StationRepository stationRepository;

    public CreateStationCommandHandler(ApplicationDbContext dbContext, StationRepository stationRepository, IMapper mapper, IBlobStorageRepository blobStorageRepository)
    {
        
        this.stationRepository = stationRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
        context = dbContext;
    }
    public  async Task<Result<Responses.GetStationResponse>> Handle(Command.CreateStationRequest request, CancellationToken cancellationToken)
    {
        
        var check = await context.Stations.Where(x => x.officeId == request.officeId 
                                                    && x.name == request.name 
                                                    && x.lat == request.lat 
                                                    && x.lng == request.lng).ToListAsync();
        if (check.Any())
        {
            throw new Exception("This station already exist!");
        }
        else
        {
            var station = new Domain.Entities.Station()
            {
                officeId = request.officeId,
                name = request.name,
                lat = request.lat,
                lng = request.lng,
            };
            
            context.AddRangeAsync(station);
            await context.SaveChangesAsync();
            var response = mapper.Map<Responses.GetStationResponse>(station);
            return await Task.FromResult(response
                );
    
        }
        
            
    }
}
