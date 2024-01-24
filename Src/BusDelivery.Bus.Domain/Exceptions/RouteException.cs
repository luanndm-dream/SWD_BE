using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDelivery.Domain.Exceptions;
public static class RouteException
{
    public class RouteBadRequestException : BadRequestException
    {
        public RouteBadRequestException(string message) : base(message)
        {
        }
    }

    public class RouteIdNotFoundException : NotFoundException
    {
        public RouteIdNotFoundException(int id)
            : base($"The Route with the id {id} was not found.") { }
    }
}
