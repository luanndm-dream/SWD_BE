using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Weather;
using BusDelivery.Infrastructure.OpenWeatherMap.Exceptions;
using BusDelivery.Infrastructure.OpenWeatherMap.Repository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Weather.Commands;
public sealed class UpsertWeatherCommandHandler : ICommandHandler<Command.Upsert>
{
    private readonly OfficeRepository officeRepository;
    private readonly WeatherRepository weatherRepository;
    public UpsertWeatherCommandHandler(
        OfficeRepository officeRepository,
        WeatherRepository weatherRepository)
    {
        this.officeRepository = officeRepository;
        this.weatherRepository = weatherRepository;

    }
    public async Task<Result> Handle(Command.Upsert request, CancellationToken cancellationToken)
    {
        var listOfficeUpdateWeather = officeRepository.FindAll(x => x.IsActive == true);
        foreach (var office in listOfficeUpdateWeather)
        {
            var openWeatherMapResponse = await weatherRepository.GetWeather5Days(office.Lat, office.Lng);
            if (openWeatherMapResponse.cod != "202" && openWeatherMapResponse.cod != "200")
                throw new WeatherException.WeatherBadRequestException("Error in Get OpenWeatherMap API");

            var listForecast = openWeatherMapResponse.list;
            await weatherRepository.UpsertWeather(office.Id, listForecast);

        }

        return Result.Success(202);
    }
}
