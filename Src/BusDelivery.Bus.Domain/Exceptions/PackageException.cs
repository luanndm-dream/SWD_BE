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
        public PackageIdNotFoundException(int PackageId)
            : base($"The Package with the id {PackageId} was not found.") { }
    }

    public class OfficeIdNotFoundException : NotFoundException
    {
        public OfficeIdNotFoundException(int OfficeId)
            : base($"The Office with the id {OfficeId} was not found.") { }
    }


    public class BusIdNotFoundException : NotFoundException
    {
        public BusIdNotFoundException(int BusId)
            : base($"The Bus with the id {BusId} was not found.") { }
    }

    public class StationIdNotFoundException : NotFoundException
    {
        public StationIdNotFoundException(int StationId)
            : base($"The Station with the id {StationId} was not found.") { }
    }

    public class DateNotFoundException : NotFoundException
    {
        public DateNotFoundException(string CreateTime)
            : base($"Day {CreateTime} was not found.") { }
    }



}
