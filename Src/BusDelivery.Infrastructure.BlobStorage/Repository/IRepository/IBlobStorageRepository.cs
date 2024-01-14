using Microsoft.AspNetCore.Http;

namespace BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
public interface IBlobStorageRepository
{
    string SaveImageOnBlobStorage(IFormFile image, string name);
    void DeleteImageFromBlobStorage(string imageUrl);
}
