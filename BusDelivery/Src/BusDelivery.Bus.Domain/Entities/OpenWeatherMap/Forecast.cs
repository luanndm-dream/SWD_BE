namespace BusDelivery.Domain.Entities.OpenWeatherMap;

public class Forecast
{
    public long dt { get; set; }
    public Main main { get; set; }
    public List<Weather> weather { get; set; }
    public Clouds clouds { get; set; }
    public Winds wind { get; set; }
    public long visibility { get; set; }
    public float pop { get; set; }
    public Sys sys { get; set; }
    public string dt_txt { get; set; }
}
