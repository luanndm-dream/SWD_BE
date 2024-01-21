namespace BusDelivery.Domain.Exceptions;
public static class RoleException
{
    public class RoleBadRequestException : BadRequestException
    {
        public RoleBadRequestException(string message) : base(message)
        {
        }
    }

    public class RoleIdNotFoundException : NotFoundException
    {
        public RoleIdNotFoundException(Guid RoleId)
            : base($"The Role with the id {RoleId} was not found.") { }
    }
}
