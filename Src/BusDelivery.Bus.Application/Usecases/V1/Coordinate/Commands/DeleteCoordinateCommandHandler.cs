using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Coordinate;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Coordinate.Commands;
public sealed class DeleteCoordinateCommandHandler : ICommandHandler<Command.DeleteCoordinateCommand>
{
    private readonly CoordinateRepository coordinateRepository;

    public DeleteCoordinateCommandHandler(CoordinateRepository coordinateRepository)
    {
        this.coordinateRepository = coordinateRepository;
    }
    public async Task<Result> Handle(Command.DeleteCoordinateCommand request, CancellationToken cancellationToken)
    {
        var existCoordinate = await coordinateRepository.FindByIdAsync(request.Id)
            ?? throw new CoordinateException.CoordinateSttNotFoundException(request.Id);
        try
        {
            coordinateRepository.Remove(existCoordinate);
            return Result.Success(202);
        }
        catch (Exception)
        {
            throw new Exception("Delete Coordinate Error");
        }
    }
}
