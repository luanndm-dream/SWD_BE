namespace BusDelivery.Domain.Exceptions;
public class PackageException
{
    public class PackageBadRequestException : BadRequestException
    {
        public PackageBadRequestException(string message) : base(message)
        {
        }
    }

    public class PackageIdNotFoundException : NotFoundException
    {
        public PackageIdNotFoundException(Guid PackageId)
            : base($"The Package with the id {PackageId} was not found.") { }
    }

    public class OfficeIdNotFoundException : NotFoundException
    {
        public OfficeIdNotFoundException(int OfficeId)
            : base($"The Office with the id {OfficeId} was not found.") { }
    }

    public class DateNotFoundException : NotFoundException
    {
        public DateNotFoundException(string CreateTime)
            : base($"Day {CreateTime} was not found.") { }
    }



}
