using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Coordinate;
public class Command
{
    public record CreateCoordinateCommand(
    string lat,
    string lng,
    int stt,
    int routeId) : ICommand<Responses.CoordinateResponses>;

    public record DeleteCoordinateCommand(int Id) : ICommand;


}
