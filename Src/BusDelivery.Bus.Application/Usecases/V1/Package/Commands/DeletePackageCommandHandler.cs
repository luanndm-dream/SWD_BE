using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Commands;
public sealed class DeletePackageCommandHandler
{
    private readonly PackageRepository packageRepository;
    public DeletePackageCommandHandler(PackageRepository packageRepository)
    {
        this.packageRepository = packageRepository;
    }

    public async Task<Result> Handle(Command.DeletePackageCommand request)
    {
        var existPackage = await packageRepository.FindByIdAsync(request.id)
            ?? throw new PackageException.PackageIdNotFoundException(request.id);
        var imageUrl = existPackage.Image;
        try
        {
            existPackage.Status = PackageStatus.Cancel;
            packageRepository.Update(existPackage);
            return Result.Success(202);
        }
        catch (Exception)
        {
            throw new Exception("Delete Package Error");
        }
    }
}
