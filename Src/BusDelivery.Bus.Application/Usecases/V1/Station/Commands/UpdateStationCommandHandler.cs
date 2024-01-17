using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Station;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence;
using BusDelivery.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Application.Usecases.V1.Station.Commands;
public sealed class UpdateStationCommandHandler : ICommandHandler<Command.UpdateStationRequest, Responses.GetStationResponse>
{
    private readonly IMapper mapper;
    private readonly StationRepository stationRepository;
    public UpdateStationCommandHandler(IMapper mapper, StationRepository stationRepository)
    {
        this.mapper = mapper;
        this.stationRepository = stationRepository;
    }
    public async Task<Result<Responses.GetStationResponse>> Handle(Command.UpdateStationRequest request, CancellationToken cancellationToken)
    {
        var station = await stationRepository.FindByIdAsync(request.stationId)
            ?? throw new StationException.StationIdNotFoundException(request.stationId);
        try
        {
            station.OfficeId = request.officeId;
            station.Name = request.name ?? string.Empty;
            station.Lat = request.lat ?? string.Empty;
            station.Lng = request.lng ?? string.Empty;
        }
        catch (Exception )
        {
            throw new Exception("Update Station error!");
        }
        stationRepository.Update(station);
        var response = mapper.Map<Responses.GetStationResponse>(station);
        return Result.Success(response);

     
        
        
    }
}
