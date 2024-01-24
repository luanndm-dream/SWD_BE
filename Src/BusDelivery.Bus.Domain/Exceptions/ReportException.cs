using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDelivery.Domain.Exceptions;
public class ReportException
{
    public class ReportBadRequestException : BadRequestException
    {
        public ReportBadRequestException(string message) : base(message)
        {
        }
    }

    public class ReportIdNotFoundException : NotFoundException
    {
        public ReportIdNotFoundException(int id)
            : base($"The Report with the id {id} was not found.") { }
    }
}
