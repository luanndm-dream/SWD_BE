using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDelivery.Domain.Exceptions;
public static class StationException
{
    public class StationBadRequestException : BadRequestException
    {
        public StationBadRequestException(string message) : base(message)
        {
        }
    }

    public class StationIdNotFoundException : NotFoundException
    {
        public StationIdNotFoundException(int stationId)
            : base($"The Station with the id {stationId} was not found.") { }
    }
}
