using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Office.Queries;
public sealed class GetOfficeByIdQueryHandler : IQueryHandler<Query.GetOfficeByIdQuery, Responses.OfficeResponse>
{
    private readonly OfficeRepository officeRepository;
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly IMapper mapper;

    public GetOfficeByIdQueryHandler(
        OfficeRepository officeRepository,
        IMapper mapper,
        IBlobStorageRepository blobStorageRepository)
    {
        this.officeRepository = officeRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }

    public async Task<Result<Responses.OfficeResponse>> Handle(Query.GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await officeRepository.FindByIdAsync(request.officeId)
            ?? throw new OfficeException.OfficeIdNotFoundException(request.officeId);
        // Convert ToBase64String
        result.Image = await blobStorageRepository.GetImageToBase64(result.Image);

        var resultResponse = mapper.Map<Responses.OfficeResponse>(result);
        return Result.Success(resultResponse);
    }
}
