using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Weather;
public static class Query
{
    public record GetWeatherQuery(int officeId) : IQuery<IReadOnlyCollection<Responses.GetWeatherResponse>>;
}
