namespace BusDelivery.Domain.Exceptions;
public class CoordinateException
{
    public class CoordinateBadRequestException : BadRequestException
    {
        public CoordinateBadRequestException(string message) : base(message)
        {
        }
    }
    public class CoordinateSttNotFoundException : NotFoundException
    {
        public CoordinateSttNotFoundException(int CoordinateStt)
            : base($"The Coordinate with the Stt {CoordinateStt} was not found.") { }
    }
}
