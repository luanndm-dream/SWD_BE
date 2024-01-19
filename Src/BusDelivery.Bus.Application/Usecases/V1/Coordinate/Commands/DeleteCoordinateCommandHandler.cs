using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Coordinate;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Coordinate.Commands;
public sealed class DeleteCoordinateCommandHandler : ICommandHandler<Command.DeleteCoordinateCommand>
{
    private readonly CoordinateRepository coordinateRepository;
    public async Task<Result> Handle(Command.DeleteCoordinateCommand request, CancellationToken cancellationToken)
    {
        var existCoordinate = await coordinateRepository.FindByIdAsync(request.id);
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
