using Microsoft.AspNetCore.Http;

namespace BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
public interface IBlobStorageRepository
{
    Task<string> SaveImageOnBlobStorage(IFormFile image, string name, string type);
    Task DeleteImageFromBlobStorage(string imageUrl);
    Task<string?> GetImageToBase64(string imageUrl);
    Task<string?> GetResizeImageToBase64(string imageUrl, int width, int height);
}
