using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Commands;
public sealed class DeletePackageCommandHandler : ICommandHandler<Command.DeletePackageCommand>
{

    private readonly PackageRepository packageRepository;
    public DeletePackageCommandHandler(PackageRepository packageRepository)
    {
        this.packageRepository = packageRepository;
    }

    public async Task<Result> Handle(Command.DeletePackageCommand request, CancellationToken cancellationToken)
    {
        var existPackage = await packageRepository.FindByIdAsync(request.id)
            ?? throw new PackageException.PackageIdNotFoundException(request.id);
        try
        {
            existPackage.Status = PackageStatus.Delete;
            packageRepository.Update(existPackage);
            return Result.Success(202);
        }
        catch (Exception)
        {
            throw new Exception("Delete Package Error");
        }
    }
}
