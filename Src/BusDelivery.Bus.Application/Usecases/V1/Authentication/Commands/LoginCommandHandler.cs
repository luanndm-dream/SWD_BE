﻿using System.IdentityModel.Tokens.Jwt;
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
        var user = await userRepository.FindByEmailAsync(request.Email)
            ?? throw new AuthException.AuthEmailNotFoundException(request.Email);

        if (user.IsActive == false)
            throw new AuthException.AuthBadRequestException("Account is not active");
        // Check Password
        if (!userRepository.VerifyPassword(user.HashPassword, request.Password))
            throw new AuthException.AuthBadRequestException("Password Incorrect");

        try
        {
            var role = await roleRepository.FindByIdAsync(user.RoleId);
            // If user -> CheckLogin
            if (!string.Equals(role.Name.Trim().ToLower(), "admin", StringComparison.OrdinalIgnoreCase))
            {
                if (IsFirstLogin(user))
                {
                    user.DeviceId = request.DeviceId;
                    user.DeviceVersion = request.DeviceVersion;
                    user.OS = request.OS;
                    userRepository.Update(user);
                }
                //else if (!CheckDevice(user, request))
                //    throw new AuthException.AuthBadRequestException("Can not login on other device");
            }
            // GetToken
            var token = await userRepository.GenerateToken(user, role.Name);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            // loginResponse
            // Add Role Description
            var loginResponse = new Responses.LoginResponses(
                user.Id,
                user.RoleId,
                role.Description,
                user.OfficeId,
                user.Name,
                user.Email,
                user.PhoneNumber,
                user.Identity,
                user.Gentle,
                request.DeviceId,
                request.DeviceVersion,
                request.OS,
                user.CreateTime.ToString("dd/MM/yyyy"),
                user.IsActive,
                tokenString,
                token.ValidTo.ToString("dd/MM/yyyy"));
            return Result.Success(loginResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool CheckDevice(Domain.Entities.User user, Command.LoginCommand request)
        => user.DeviceId == request.DeviceId
        && user.DeviceVersion == request.DeviceVersion
        && user.OS == request.OS;

    private bool IsFirstLogin(Domain.Entities.User user)
        => string.IsNullOrWhiteSpace(user.DeviceId)
        || string.IsNullOrWhiteSpace(user.DeviceVersion)
        || user.OS == null;
}
