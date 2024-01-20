using BusDelivery.Domain.Exceptions;

namespace BusDelivery.Infrastructure.OpenWeatherMap.Exceptions;
public static class WeatherException
{
    public class WeatherBadRequestException : BadRequestException
    {
        public WeatherBadRequestException(string message) : base(message)
        {
        }
    }

    public class WeatherNotFoundException : NotFoundException
    {
        public WeatherNotFoundException(int officeId)
            : base($"The Weather of office {officeId} was not found.") { }
    }
}
