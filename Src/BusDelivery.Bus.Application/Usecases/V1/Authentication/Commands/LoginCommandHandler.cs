using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Authentication;
using BusDelivery.Persistence.Repositories;
using Microsoft.AspNetCore.Http;

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
        var user = await userRepository.FindByEmailAsync(request.email);
        if (user is null)
            return Result.Failure<Responses.LoginResponses>($"Email {request.email} was not register!", StatusCodes.Status404NotFound);
        // Check Password
        if (!userRepository.VerifyPassword(user.HashPassword, request.password))
            return Result.Failure<Responses.LoginResponses>("Password Incorrect", StatusCodes.Status400BadRequest);

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
            user.OS,
            user.IsDeleted,
            user.IsActive,
            new JwtSecurityTokenHandler().WriteToken(token),
            token.ValidTo);
        return Result.Success(loginResponse);
    }
}
