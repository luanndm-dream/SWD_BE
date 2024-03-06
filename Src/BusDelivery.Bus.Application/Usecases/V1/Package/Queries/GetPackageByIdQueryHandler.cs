using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Package.Queries;
public sealed class GetPackageByIdQueryHandler : IQueryHandler<Query.GetPackageByIdQuery, Responses.PackageResponse>
{
    private readonly PackageRepository packageRepository;
    private readonly IMapper mapper;

    public GetPackageByIdQueryHandler(
        PackageRepository packageRepository,
        IMapper mapper)
    {
        this.packageRepository = packageRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.PackageResponse>> Handle(Query.GetPackageByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await packageRepository.FindByIdAsync(request.packageId)
            ?? throw new PackageException.PackageIdNotFoundException(request.packageId);

        var resultResponse = mapper.Map<Responses.PackageResponse>(result);
        return Result.Success(resultResponse);
    }
}
