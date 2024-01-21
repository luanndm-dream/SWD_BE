using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Coordinate;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Coordinate.Commands;
public sealed class CreateCoordinateCommandHandler : ICommandHandler<Command.CreateCoordinateCommand, Responses.CoordinateResponses>
{
    private readonly CoordinateRepository coordinateRepository;
    private readonly IMapper mapper;


    public CreateCoordinateCommandHandler(CoordinateRepository coordinateRepository, IMapper mapper)
    {
        this.coordinateRepository = coordinateRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.CoordinateResponses>> Handle(Command.CreateCoordinateCommand request, CancellationToken cancellationToken)
    {
        var coordinate = new Domain.Entities.Coordinate
        {
            Lat = request.lat,
            Lng = request.lng,
            Stt = request.stt,
            RouteId = request.routeId,
        };
        try
        {
            coordinateRepository.Add(coordinate);
            var coordinateResponse = mapper.Map<Responses.CoordinateResponses>(coordinate);
            return Result.Success(coordinateResponse, 201);

        }
        catch (Exception)
        {
            throw new Exception("Create Coordinate Error");
        }

    }
}
