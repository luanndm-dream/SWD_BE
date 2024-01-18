using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Authentication;
using BusDelivery.Domain.Entities;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Authentication.Commands;
public sealed class RegisterCommandHandler : ICommandHandler<Command.RegisterCommand, Responses.UserReponses>
{
    private readonly UserRepository userRepository;
    private readonly IMapper mapper;

    public RegisterCommandHandler(UserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.UserReponses>> Handle(Command.RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check Email was Register
        var UserWithEmailExist = await userRepository.FindByEmailAsync(request.email)
            ?? throw new AuthException.AuthBadRequestException("Email was register!");

        // HashPassword
        var hashPassword = userRepository.HashPassword(request.password);

        // Create UserEntity
        var user = new User
        {
            RoleId = request.roleId,
            OfficeId = request.officeId,
            Name = request.name,
            Email = request.email,
            HashPassword = hashPassword,
            PhoneNumber = request.phoneNumber,
            Gentle = request.gentle,
            DeviceId = request.deviceId,
            DeviceVersion = request.deviceVersion,
            OS = request.OS,
            IsDeleted = request.IsDeleted,
            IsActive = request.IsActive,
            CreateTime = DateTime.Now
        };

        try
        {
            userRepository.Add(user);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        // MapToResponse
        var userResponse = mapper.Map<Responses.UserReponses>(user);
        return Result.Success(userResponse);
    }
}
