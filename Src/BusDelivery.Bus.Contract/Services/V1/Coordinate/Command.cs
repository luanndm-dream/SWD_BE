using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Coordinate;
public class Command
{
    public record CreateCoordinateCommand(
    string lat,
    string lng,
    int stt,
    int routeId,
    bool status) : ICommand<Responses.CoordinateResponses>;

    public record UpdateCoordinateCommand(
    int id,
    string lat,
    string lng,
    int stt,
    int routeId,
    bool status) : ICommand<Responses.CoordinateResponses>;

    public record DeleteCoordinateCommand(int id) : ICommand;


}
