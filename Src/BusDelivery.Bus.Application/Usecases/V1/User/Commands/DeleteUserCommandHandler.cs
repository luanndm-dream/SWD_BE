using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Commands;
public sealed class DeleteUserCommandHandler : ICommandHandler<Command.DeleteUserCommand>
{
    private readonly UserRepository userRepository;
    private readonly RoleRepository roleRepository;
    public DeleteUserCommandHandler(
        UserRepository userRepository,
        RoleRepository roleRepository)
    {
        this.userRepository = userRepository;
        this.roleRepository = roleRepository;
    }
    public async Task<Result> Handle(Command.DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userExist = await userRepository.FindByIdAsync(request.Id)
            ?? throw new UserException.UserIdNotFoundException(request.Id);
        // Can not delete Admin
        if (await roleRepository.IsAdmin(userExist.RoleId))
            throw new UserException.UserBadRequestException("Can not delete Admin");

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
