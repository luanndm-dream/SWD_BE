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
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Station.Queries;
public sealed class GetStationByIdHandler : IQueryHandler<Query.GetStationById, Responses.GetStationResponse>
{
    private readonly StationRepository stationRepository;
    private readonly IMapper mapper;
    
    public GetStationByIdHandler(StationRepository stationRepository, IMapper mapper)
    {
        this.stationRepository = stationRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.GetStationResponse>> Handle(Query.GetStationById request, CancellationToken cancellationToken)
    {
        var result = await stationRepository.FindByIdAsync(request.id)
            ?? throw new StationException.StationIdNotFoundException(request.id);

        var response = mapper.Map<Responses.GetStationResponse>(result);
        return Result.Success(response);
    }
}
