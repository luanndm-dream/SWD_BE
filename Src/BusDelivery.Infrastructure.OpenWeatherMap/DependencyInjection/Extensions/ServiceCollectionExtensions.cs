using BusDelivery.Infrastructure.OpenWeatherMap.DependencyInjection.Options;
using BusDelivery.Infrastructure.OpenWeatherMap.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BusDelivery.Infrastructure.OpenWeatherMap.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigInfrastructureOpenWeather(this IServiceCollection services)
        => services.AddTransient<WeatherRepository>();

    public static OptionsBuilder<OpenWeatherMapOptions> ConfigureOpenWeatherMapOptions(this IServiceCollection services, IConfigurationSection section)
    => services.AddOptions<OpenWeatherMapOptions>()
        .Bind(section)
        .ValidateDataAnnotations()
        .ValidateOnStart();
}
