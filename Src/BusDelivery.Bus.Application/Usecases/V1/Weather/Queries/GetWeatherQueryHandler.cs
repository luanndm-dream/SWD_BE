using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Weather;
using BusDelivery.Infrastructure.OpenWeatherMap.Exceptions;
using BusDelivery.Infrastructure.OpenWeatherMap.Repository;

namespace BusDelivery.Application.Usecases.V1.Weather.Queries;
public sealed class GetWeatherQueryHandler : IQueryHandler<Query.GetWeatherQuery, IReadOnlyCollection<Responses.GetWeatherResponse>>
{
    private readonly WeatherRepository weatherRepository;
    private readonly IMapper mapper;
    public GetWeatherQueryHandler(WeatherRepository weatherRepository, IMapper mapper)
    {
        this.weatherRepository = weatherRepository;
        this.mapper = mapper;
    }

    public async Task<Result<IReadOnlyCollection<Responses.GetWeatherResponse>>> Handle(Query.GetWeatherQuery request, CancellationToken cancellationToken)
    {
        var result = weatherRepository.GetListWeatherByOfficeIdAsync(request.officeId)
            ?? throw new WeatherException.WeatherNotFoundException(request.officeId);

        var resultResponse = mapper.Map<IReadOnlyCollection<Responses.GetWeatherResponse>>(result);
        return Result.Success(resultResponse);
    }
}
