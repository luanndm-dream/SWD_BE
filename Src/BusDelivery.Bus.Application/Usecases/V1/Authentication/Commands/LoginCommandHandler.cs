using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Authentication;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Authentication.Commands;
public sealed class LoginCommandHandler : ICommandHandler<Command.LoginCommand, Responses.LoginResponses>
{
    private readonly IMapper mapper;
    private readonly UserRepository userRepository;
    private readonly RoleRepository roleRepository;
    public LoginCommandHandler(UserRepository userRepository, IMapper mapper, RoleRepository roleRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.roleRepository = roleRepository;
    }

    public async Task<Result<Responses.LoginResponses>> Handle(Command.LoginCommand request, CancellationToken cancellationToken)
    {
        // Check Email is register?
        var user = await userRepository.FindByEmailAsync(request.email)
            ?? throw new AuthException.AuthEmailNotFoundException(request.email);
        // Check Password
        if (!userRepository.VerifyPassword(user.HashPassword, request.password))
            throw new AuthException.AuthBadRequestException("Password Incorrect");

        // GetRoleName
        var roleName = roleRepository.FindByIdAsync(user.RoleId).GetAwaiter().GetResult().Name;
        // GetToken
        var token = await userRepository.GenerateToken(user, roleName);

        // loginResponse
        var loginResponse = new Responses.LoginResponses(
            user.Id,
            user.RoleId,
            user.OfficeId,
            user.Name,
            user.Email,
            user.PhoneNumber,
            user.Gentle,
            user.DeviceId,
            user.DeviceVersion,
            user.CreateTime,
            user.OS,
            user.IsActive,
            new JwtSecurityTokenHandler().WriteToken(token),
            token.ValidTo);
        return Result.Success(loginResponse);
    }
}
