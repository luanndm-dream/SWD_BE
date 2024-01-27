using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Authentication;
public class Command
{
    public record LoginCommand(
        string Email,
        string Password,
        string? DeviceId,
        string? DeviceVersion,
        OS? OS) : ICommand<Responses.LoginResponses>;

    public record ForgotPasswordCommand(
        string Email,
        string NewPassword,
        string DeviceId,
        string DeviceVersion,
        OS OS) : ICommand;
}
