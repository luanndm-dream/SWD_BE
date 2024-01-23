using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Commands;
public sealed class CreateUserCommandHandler : ICommandHandler<Command.CreateUserCommand, Responses.UserResponse>
{
    private readonly UserRepository userRepository;
    private readonly RoleRepository roleRepository;
    private readonly OfficeRepository officeRepository;
    private readonly IMapper mapper;

    public CreateUserCommandHandler(UserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.UserResponse>> Handle(Command.CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Check Email was Register
        var UserWithEmailExist = await userRepository.FindByEmailAsync(request.Email);
        if (UserWithEmailExist != null)
            throw new UserException.UserBadRequestException("Email was exist");

        var userWithIdentityExist = await userRepository.FindByIdentityAsync(request.Identity);
        if (userWithIdentityExist != null)
            throw new UserException.UserBadRequestException("Identity was exist");


        var roleExist = await roleRepository.FindByIdAsync(request.RoleId)
            ?? throw new RoleException.RoleIdNotFoundException(request.RoleId);

        var officeExist = await officeRepository.FindByIdAsync(request.OfficeId)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.OfficeId);

        // HashPassword
        var hashPassword = userRepository.HashPassword(request.Password);

        // Create UserEntity
        var user = new Domain.Entities.User
        {
            RoleId = request.RoleId,
            OfficeId = request.OfficeId,
            Name = request.Name,
            Email = request.Email,
            HashPassword = hashPassword,
            PhoneNumber = request.PhoneNumber,
            Identity = request.Identity,
            Gentle = request.Gentle,
            IsActive = true,
            CreateTime = DateTime.Now.ToString("dd/MM/yyyy")
        };

        try
        {
            userRepository.Add(user);
            var userResponse = mapper.Map<Responses.UserResponse>(user);
            return Result.Success(userResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
