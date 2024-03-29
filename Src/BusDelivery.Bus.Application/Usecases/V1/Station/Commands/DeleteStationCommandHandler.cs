﻿using System;
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

namespace BusDelivery.Application.Usecases.V1.Station.Commands;
public sealed class DeleteStationCommandHandler : ICommandHandler<Command.DeleteStationRequest>
{
    private readonly StationRepository stationRepository;
    private readonly IMapper mapper;
    public DeleteStationCommandHandler(StationRepository stationRepository,IMapper mapper)
    {
        this.stationRepository = stationRepository;
        this.mapper = mapper;   
    }

    public async Task<Result> Handle(Command.DeleteStationRequest request, CancellationToken cancellationToken)
    {
        var station = await stationRepository.FindByIdAsync(request.stationId)
            ?? throw new StationException.StationIdNotFoundException(request.stationId);
        try
        {
            station.IsActive = false;
            stationRepository.Update(station);
            return Result.Success(202);
        }
        catch
        {
            throw new Exception("Delete Station not success!");
        }

    }
}
