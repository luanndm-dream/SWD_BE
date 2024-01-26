using BusDelivery.Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace BusDelivery.Contract.Services.V1.Order;
public class Command
{
    public record CreateOrderCommand(
    int packageid,
    IFormFile image,
    float weight,
    float price,
    string note,
    string contact) : ICommand<Responses.OrderResponses>;

    public record UpdateOrderCommand(
    int id,
    int packageid,
    IFormFile image,
    float weight,
    float price,
    string note,
    string contact) : ICommand<Responses.OrderResponses>;

    public record DeleteOrderCommand(int id) : ICommand;
}
