using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Authentication;
public class Command
{
    public record RegisterCommand(
        Guid roleId,
        int officeId,
        string name,
        string email,
        string password,
        string phoneNumber,
        Gentle gentle,
        string deviceId,
        string deviceVersion,
        string OS,
        bool IsDeleted,
        bool IsActive) : ICommand<Responses.UserReponses>;

    public record LoginCommand(
        string email,
        string password) : ICommand<Responses.LoginResponses>;
}
