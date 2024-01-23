using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Commands;
public sealed class DeleteUserCommandHandler : ICommandHandler<Command.DeleteUserCommand>
{
    private readonly UserRepository userRepository;
    public DeleteUserCommandHandler(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task<Result> Handle(Command.DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userExist = await userRepository.FindByIdAsync(request.Id)
            ?? throw new UserException.UserIdNotFoundException(request.Id);

        try
        {
            userRepository.Delete(userExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
