namespace BusDelivery.Contract.Services.V1.Weather;
public static class Responses
{
    public record GetWeatherResponse(
        double Temperature,
        double Humidity,
        double WindySpeed,
        string RecordAt);
}
