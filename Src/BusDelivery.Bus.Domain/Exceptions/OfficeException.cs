namespace BusDelivery.Domain.Exceptions;
public static class OfficeException
{
    public class OfficeBadRequestException : BadRequestException
    {
        public OfficeBadRequestException(string message) : base(message)
        {
        }
    }

    public class OfficeIdNotFoundException : NotFoundException
    {
        public OfficeIdNotFoundException(int OfficeId)
            : base($"The Office with the id {OfficeId} was not found.") { }
    }
}
