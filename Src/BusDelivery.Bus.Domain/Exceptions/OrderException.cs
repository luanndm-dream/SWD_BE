namespace BusDelivery.Domain.Exceptions;
public class OrderException
{
    public class OrderBadRequestException : BadRequestException
    {
        public OrderBadRequestException(string message) : base(message)
        {
        }
    }

    public class OrderIdNotFoundException : NotFoundException
    {
        public OrderIdNotFoundException(int OrderId)
            : base($"The Order with the id {OrderId} was not found.") { }
    }
}
