using BusDelivery.Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace BusDelivery.Contract.Services.V1.Office;
public class Command
{
    public record CreateOfficeCommand(
        int routeId,
        string name,
        string address,
        string lat,
        string lng,
        string contact,
        IFormFile image,
        bool status) : ICommand<Responses.OfficeReponses>;

    public record UpdateOfficeCommand(
        int id,
        int routeId,
        string name,
        string address,
        string lat,
        string lng,
        string contact,
        IFormFile image,
        bool status) : ICommand<Responses.OfficeReponses>;

    public record DeleteOfficeCommand(int id) : ICommand;
}
