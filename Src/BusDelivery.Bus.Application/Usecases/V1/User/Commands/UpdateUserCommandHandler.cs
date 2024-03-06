using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.User;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.User.Commands;
public sealed class UpdateUserCommandHandler : ICommandHandler<Command.UpdateUserCommand, Responses.UserResponse>
{
    private readonly UserRepository userRepository;
    private readonly RoleRepository roleRepository;
    private readonly OfficeRepository officeRepository;
    private readonly IBlobStorageRepository blobStorageRepository;

    public UpdateUserCommandHandler(
        UserRepository userRepository,
        RoleRepository roleRepository,
        OfficeRepository officeRepository,
        IBlobStorageRepository blobStorageRepository)
    {
        this.userRepository = userRepository;
        this.roleRepository = roleRepository;
        this.officeRepository = officeRepository;
        this.blobStorageRepository = blobStorageRepository;
    }
    public async Task<Result<Responses.UserResponse>> Handle(Command.UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userExist = await userRepository.FindByIdAsync(request.Id.Value)
            ?? throw new UserException.UserIdNotFoundException(request.Id.Value);

        var roleExist = await roleRepository.FindByIdAsync(request.RoleId)
            ?? throw new RoleException.RoleIdNotFoundException(request.RoleId);

        var officeExist = await officeRepository.FindByIdAsync(request.OfficeId)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.OfficeId);

        // Delete oldAvatar and Upload newAvatar
        var oldAvatarUrl = userExist.Avatar;


        // Save newAvatar and GetNewAvatarUrl
        var newAvatarUrl = await blobStorageRepository.SaveImageOnBlobStorage(request.Avatar, request.Name, "avatars")
            ?? throw new Exception("Upload File fail");

        try
        {
            userExist.Update(
                request.Id.Value,
                request.RoleId,
                request.OfficeId,
                request.Name,
                request.Email,
                request.PhoneNumber,
                request.Identity,
                request.Gentle,
                request.DeviceId,
                request.DeviceVersion,
                request.OS,
                request.IsActive,
                newAvatarUrl);

            userRepository.Update(userExist);
            var userResponse = userExist.ToResponses(roleExist.Description);
            if (!string.IsNullOrEmpty(oldAvatarUrl))
                blobStorageRepository.DeleteImageFromBlobStorage(oldAvatarUrl);

            return Result.Success(userResponse, 202);
        }
        catch (Exception ex)
        {
            await blobStorageRepository.DeleteImageFromBlobStorage(newAvatarUrl);
            throw new Exception(ex.Message);
        }
    }
}
