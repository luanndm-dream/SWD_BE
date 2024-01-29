namespace BusDelivery.Domain.Exceptions;
public static class UserException
{
    public class UserBadRequestException : BadRequestException
    {
        public UserBadRequestException(string message) : base(message)
        {
        }
    }

    public class UserIdNotFoundException : NotFoundException
    {
        public UserIdNotFoundException(int UserId)
            : base($"The User with the id {UserId} was not found.") { }
    }
}
