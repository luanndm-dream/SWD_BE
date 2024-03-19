using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Commands;
public sealed class CreateUserCommandHandler : ICommandHandler<Command.CreateUserCommand, Responses.UserResponse>
{
    private readonly UserRepository userRepository;
    private readonly RoleRepository roleRepository;
    private readonly OfficeRepository officeRepository;
    private readonly ApplicationDbContext context;
    private readonly IBlobStorageRepository blobStorageRepository;

    public CreateUserCommandHandler(
        UserRepository userRepository,
        RoleRepository roleRepository,
        OfficeRepository officeRepository,
        ApplicationDbContext context,
        IBlobStorageRepository blobStorageRepository)
    {
        this.userRepository = userRepository;
        this.roleRepository = roleRepository;
        this.officeRepository = officeRepository;
        this.context = context;
        this.blobStorageRepository = blobStorageRepository;
    }
    public async Task<Result<Responses.UserResponse>> Handle(Command.CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Can not Create Admin
        if (await roleRepository.IsAdmin(request.RoleId))
            throw new UserException.UserBadRequestException("Can not Create Admin");

        // Check Email was Register
        var UserWithEmailExist = await userRepository.FindByEmailAsync(request.Email);
        if (UserWithEmailExist != null)
            throw new UserException.UserBadRequestException("Email was exist");

        var userWithIdentityExist = await userRepository.FindByIdentityAsync(request.Identity);
        if (userWithIdentityExist != null)
            throw new UserException.UserBadRequestException("Identity was exist");

        // Check RoleExist
        var roleExist = await roleRepository.FindByIdAsync(request.RoleId)
            ?? throw new RoleException.RoleIdNotFoundException(request.RoleId);
        // Check OfficeExist
        var officeExist = await officeRepository.FindByIdAsync(request.OfficeId)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.OfficeId);

        // HashPassword
        var hashPassword = userRepository.HashPassword(request.Password);

        // SaveImageInBlob
        var nameImage = userRepository.GetFirstPartEmail(request.Email);
        var avatarUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.Avatar, nameImage, "avatars")
            ?? throw new Exception("Upload File fail");

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
            CreateTime = DateTime.Now,
            Avatar = avatarUrl
        };

        try
        {
            userRepository.Add(user);
            await context.SaveChangesAsync();

            var userResponse = user.ToResponses(roleExist.Description);
            return Result.Success(userResponse, 201);
        }
        catch (Exception ex)
        {
            await blobStorageRepository.DeleteImageFromBlobStorage(avatarUrl);
            throw new Exception(ex.Message);
        }
    }
}
