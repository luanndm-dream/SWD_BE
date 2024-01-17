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
    private readonly IMapper mapper;
    private readonly ApplicationDbContext context;
    private readonly StationRepository stationRepository;

    public CreateStationCommandHandler(ApplicationDbContext dbContext, StationRepository stationRepository, IMapper mapper, IBlobStorageRepository blobStorageRepository)
    {

        this.stationRepository = stationRepository;
        this.mapper = mapper;
        context = dbContext;
    }
    public async Task<Result<Responses.GetStationResponse>> Handle(Command.CreateStationRequest request, CancellationToken cancellationToken)
    {
        var station = new Domain.Entities.Station()
        {
            OfficeId = request.officeId,
            Name = request.name ?? string.Empty,
            Lat = request.lat ?? string.Empty,
            Lng = request.lng ?? string.Empty,
        };
        try
        {
            stationRepository.Add(station);
            await context.SaveChangesAsync();
            var response = mapper.Map<Responses.GetStationResponse>(station);
            return Result.Success(response,201);
        }
        catch (Exception)
        {
            throw new Exception("Add new station error");
        }

    }
}    
            
    
