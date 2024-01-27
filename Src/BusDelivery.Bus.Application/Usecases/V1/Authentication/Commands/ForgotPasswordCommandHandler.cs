using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Authentication;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Authentication.Commands;
public sealed class ForgotPasswordCommandHandler : ICommandHandler<Command.ForgotPasswordCommand>
{
    private readonly UserRepository userRepository;
    public ForgotPasswordCommandHandler(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task<Result> Handle(Command.ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.FindByEmailAsync(request.Email);
            if (!CheckDevice(user, request))
                throw new AuthException.AuthBadRequestException("Can not change on other device");

            var hashPassword = userRepository.HashPassword(request.NewPassword);
            user.HashPassword = hashPassword;

            userRepository.Update(user);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool CheckDevice(Domain.Entities.User user, Command.ForgotPasswordCommand request)
    => user.DeviceId == request.DeviceId
    && user.DeviceVersion == request.DeviceVersion
    && user.OS == request.OS;

}
