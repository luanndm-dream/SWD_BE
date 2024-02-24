using BusDelivery.Infrastructure.OpenWeatherMap.Exceptions;
using BusDelivery.Infrastructure.OpenWeatherMap.Repository;
using BusDelivery.Persistence.Repositories;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;

namespace BusDelivery.Infrastructure.OpenWeatherMap;
public class WeatherScheduler : IInvocable
{
    private readonly WeatherRepository weatherRepository;
    private readonly OfficeRepository officeRepository;
    private readonly ILogger<WeatherScheduler> logger;
    public WeatherScheduler(
    WeatherRepository weatherRepository,
    OfficeRepository officeRepository,
    ILogger<WeatherScheduler> logger)
    {
        this.weatherRepository = weatherRepository;
        this.officeRepository = officeRepository;
        this.logger = logger;
    }
    public async Task Invoke()
    {
        var listOfficeUpdateWeather = officeRepository.FindAll(x => x.IsActive == true);
        foreach (var office in listOfficeUpdateWeather)
        {
            var openWeatherMapResponse = await weatherRepository.GetWeather5Days(office.Lat, office.Lng);
            if (openWeatherMapResponse.cod != "202" && openWeatherMapResponse.cod != "200")
                throw new WeatherException.WeatherBadRequestException("Error in Update Weather");

            var listForecast = openWeatherMapResponse.list;
            await weatherRepository.UpsertWeather(office.Id, listForecast);
        }
        Console.WriteLine("Update Weather Successful");
        logger.LogInformation("Weather has been update");
    }
}
