using System.Runtime.Serialization.Json;
using System.Text;
using BusDelivery.Infrastructure.OpenWeatherMap.DependencyInjection.Options;
using BusDelivery.Persistence;
using BusDelivery.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BusDelivery.Infrastructure.OpenWeatherMap.Repository;
public class WeatherRepository : RepositoryBase<Domain.Entities.Weather, int>
{
    private readonly ApplicationDbContext context;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly OpenWeatherMapOptions openWeatherMapOptions;

    public WeatherRepository(
        ApplicationDbContext context,
        IHttpClientFactory httpClientFactory,
        IOptions<OpenWeatherMapOptions> openWeatherMapOptions) : base(context)
    {
        this.context = context;
        this.httpClientFactory = httpClientFactory;
        this.openWeatherMapOptions = openWeatherMapOptions.Value;
    }

    public IReadOnlyCollection<Domain.Entities.Weather> GetListWeatherByOfficeIdAsync(int officeId)
    {
        var result = context.Weather.AsNoTracking().Where(x => x.OfficeId == officeId).ToList();
        return result;
    }

    public async Task<OpenWeatherMapResponse> GetWeather5Days(string lat, string lon)
    {
        var client = httpClientFactory.CreateClient();
        string uri = $"{openWeatherMapOptions.BaseUri}/{openWeatherMapOptions.Option}" +
            $"?appid={openWeatherMapOptions.Apikey}&units={openWeatherMapOptions.Units}" +
            $"&lat={lat}&lon={lon}";
        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await client.SendAsync(message);
        var json = await response.Content.ReadAsStringAsync();

        using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json.ToString())))
        {
            var serializer = new DataContractJsonSerializer(typeof(OpenWeatherMapResponse));
            var responseOpenWeather = (OpenWeatherMapResponse)serializer.ReadObject(ms);
            return responseOpenWeather;
        }
    }

    public async Task UpsertWeather(int officeId, List<Forecast> listForecast)
    {
        foreach (var item in listForecast)
        {
            var weather = new Domain.Entities.Weather
            {
                OfficeId = officeId,
                Humidity = item.main.humidity,
                Temperature = item.main.temp,
                WindySpeed = item.wind.speed,
                RecordAt = item.dt.ToString()
            };

            var existWeather = await context.Weather.FirstOrDefaultAsync(x => x.OfficeId == officeId && x.RecordAt == weather.RecordAt);
            if (existWeather == null)
                Add(existWeather);
            Update(existWeather);
        }
    }
}
