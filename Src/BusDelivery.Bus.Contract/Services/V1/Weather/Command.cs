using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Weather;
public static class Command
{
    public record Upsert() : ICommand;
}
