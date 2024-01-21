using BusDelivery.Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace BusDelivery.Contract.Services.V1.Office;
public class Command
{
    public record CreateOfficeCommand(
        string name,
        string address,
        string lat,
        string lng,
        string contact,
        IFormFile image,
        bool status) : ICommand<Responses.OfficeResponse>;

    public record UpdateOfficeCommand(
        int id,
        string name,
        string address,
        string lat,
        string lng,
        string contact,
        IFormFile image,
        bool status) : ICommand<Responses.OfficeResponse>;

    public record DeleteOfficeCommand(int id) : ICommand;
}
