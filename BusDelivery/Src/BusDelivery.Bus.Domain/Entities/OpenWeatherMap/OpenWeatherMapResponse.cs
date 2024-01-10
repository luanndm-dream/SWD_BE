namespace BusDelivery.Domain.Entities.OpenWeatherMap;

public class OpenWeatherMapResponse
{
    public string cod { get; set; }
    public int message { get; set; }
    public int cnt { get; set; }
    public List<Forecast> list { get; set; }
}
