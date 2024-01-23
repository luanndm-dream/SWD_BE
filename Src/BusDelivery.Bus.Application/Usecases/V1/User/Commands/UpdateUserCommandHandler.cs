using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Commands;
public sealed class UpdateUserCommandHandler : ICommandHandler<Command.UpdateUserCommand, Responses.UserResponse>
{
    private readonly UserRepository userRepository;
    private readonly RoleRepository roleRepository;
    private readonly OfficeRepository officeRepository;
    private readonly IMapper mapper;

    public UpdateUserCommandHandler(UserRepository userRepository, IMapper mapper, RoleRepository roleRepository, OfficeRepository officeRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.roleRepository = roleRepository;
        this.officeRepository = officeRepository;
    }
    public async Task<Result<Responses.UserResponse>> Handle(Command.UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userExist = await userRepository.FindByIdAsync(request.Id)
            ?? throw new UserException.UserIdNotFoundException(request.Id);

        var roleExist = await roleRepository.FindByIdAsync(request.RoleId)
            ?? throw new RoleException.RoleIdNotFoundException(request.RoleId);

        var officeExist = await officeRepository.FindByIdAsync(request.OfficeId)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.OfficeId);

        try
        {
            var hashPass = userRepository.HashPassword(request.Password);

            userExist.Update(
                request.Id,
                request.RoleId,
                request.OfficeId,
                request.Name,
                request.Email,
                hashPass,
                request.PhoneNumber,
                request.Identity,
                request.Gentle,
                request.DeviceId,
                request.DeviceVersion,
                request.OS,
                request.CreateTime,
                request.IsActive);

            userRepository.Update(userExist);
            var userResponse = mapper.Map<Responses.UserResponse>(userExist);
            return Result.Success(userResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
