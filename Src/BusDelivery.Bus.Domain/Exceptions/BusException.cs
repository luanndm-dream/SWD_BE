using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDelivery.Domain.Exceptions;
public class BusException
{
    public class BusBadRequestException : BadRequestException
    {
        public BusBadRequestException(string message) : base(message)
        {
        }
    }

    public class BusIdNotFoundException : NotFoundException
    {
        public BusIdNotFoundException(int busId)
            : base($"The Bua with the id {busId} was not found.") { }
    }
}


