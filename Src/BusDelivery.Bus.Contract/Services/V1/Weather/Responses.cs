namespace BusDelivery.Contract.Services.V1.Weather;
public static class Responses
{
    public record GetWeatherResponse(double temperature, double humidity, double windySpeed, string recordAt);
}
