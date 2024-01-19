namespace BusDelivery.Domain.Exceptions;
public static class AuthException
{
    public class AuthBadRequestException : BadRequestException
    {
        public AuthBadRequestException(string message) : base(message)
        {
        }
    }

    public class AuthIdNotFoundException : NotFoundException
    {
        public AuthIdNotFoundException(int userId)
            : base($"The User with the id {userId} was not found.") { }
    }

    public class AuthEmailNotFoundException : NotFoundException
    {
        public AuthEmailNotFoundException(string email)
            : base($"The User with the email {email} was not found.") { }
    }
}
