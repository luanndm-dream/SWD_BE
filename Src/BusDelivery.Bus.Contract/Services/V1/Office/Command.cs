using BusDelivery.Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace BusDelivery.Contract.Services.V1.Office;
public class Command
{
    public record CreateOfficeCommand(
        string Name,
        string Address,
        string Lat,
        string Lng,
        string Contact,
        IFormFile Image,
        bool IsActive) : ICommand<Responses.OfficeResponse>;

    public record UpdateOfficeCommand(
        int? Id,
        string Name,
        string Address,
        string Lat,
        string Lng,
        string Contact,
        IFormFile Image,
        bool IsActive) : ICommand<Responses.OfficeResponse>;

    public record DeleteOfficeCommand(int id) : ICommand;
}
