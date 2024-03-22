using AutoMapper;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Package;
using BusDelivery.Domain.Exceptions;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using BusDelivery.Persistence.Repositories;
using Microsoft.AspNetCore.Http;

namespace BusDelivery.Application.Usecases.V1.Package.Commands;
public class CreatePackageCommandHandler : ICommandHandler<Command.CreatePackageCommand, Responses.PackageResponse>
{
    private readonly IBlobStorageRepository blobStorageRepository;
    private readonly PackageRepository packageRepository;
    private readonly IMapper mapper;
    private readonly StationRepository stationRepository;
    private readonly OfficeRepository officeRepository;
    private readonly BusRepository busRepository;
    public CreatePackageCommandHandler(IBlobStorageRepository blobStorageRepository, PackageRepository packageRepository, StationRepository stationRepository, OfficeRepository officeRepository, BusRepository busRepository, IMapper mapper)
    {
        this.blobStorageRepository = blobStorageRepository;
        this.packageRepository = packageRepository;
        this.mapper = mapper;
        this.stationRepository = stationRepository;
        this.officeRepository = officeRepository;
        this.busRepository = busRepository;
    }
    public static IFormFile ConvertToIFormFile(string base64String, string fileName)
    {
        // Giải mã chuỗi Base64 thành mảng byte
        byte[] bytes = Convert.FromBase64String(base64String);

        // Tạo một luồng bộ nhớ đệm từ mảng byte
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            // Tạo một đối tượng MemoryStream từ luồng bộ nhớ đệm
            MemoryStream stream = new MemoryStream(memoryStream.ToArray());
            // Tạo một đối tượng IFormFile từ MemoryStream
            IFormFile file = new FormFile(stream, 0, bytes.Length, "file", fileName);

            return file;
        }
    }
    public async Task<Result<Responses.PackageResponse>> Handle(Command.CreatePackageCommand request, CancellationToken cancellationToken)
    {
        var image = ConvertToIFormFile(request.image, request.totalPrice.ToString());

        var imageUrl = await blobStorageRepository.SaveImageOnBlobStorage(image, $"{request.fromOfficeId}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}", "packages")
            ?? throw new Exception("Upload File fail");
        var package = new Domain.Entities.Package
        {
            BusId = request.busId,
            FromOfficeId = request.fromOfficeId,
            ToOfficeId = request.toOfficeId,
            StationId = request.stationId,
            Quantity = request.quantity,
            TotalWeight = request.totalWeight,
            TotalPrice = request.totalPrice,
            Image = imageUrl,
            Note = request.note,
            Status = request.status,
            CreateTime = DateTime.Now,
        };

        var result = await stationRepository.FindByIdAsync(request.stationId)
            ?? throw new PackageException.StationIdNotFoundException(request.stationId);

        var result1 = await officeRepository.FindByIdAsync(request.fromOfficeId)
        ?? throw new PackageException.OfficeIdNotFoundException(request.fromOfficeId);

        var result2 = await officeRepository.FindByIdAsync(request.toOfficeId)
        ?? throw new PackageException.OfficeIdNotFoundException(request.toOfficeId);

        var result3 = await busRepository.FindByIdAsync(request.busId)
        ?? throw new PackageException.BusIdNotFoundException(request.busId);


        try
        {
            packageRepository.Add(package);
            var packageResponse = mapper.Map<Responses.PackageResponse>(package);
            return Result.Success(packageResponse, 201);
        }
        catch (Exception)
        {
            await blobStorageRepository.DeleteImageFromBlobStorage(imageUrl);
            throw new Exception("Create Package Error");
        }
    }
}
