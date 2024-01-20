using Azure;
using Azure.Storage.Blobs;
using BusDelivery.Infrastructure.BlobStorage.DependencyInjection.Options;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BusDelivery.Infrastructure.BlobStorage.Repositories;
public class BlobStorageRepository : IBlobStorageRepository
{
    private readonly BlobStorageOptions blobStorageOptions;

    public BlobStorageRepository(IOptions<BlobStorageOptions> blobStorageOptions)
    {
        this.blobStorageOptions = blobStorageOptions.Value;
    }

    public async Task<string> SaveImageOnBlobStorage(IFormFile image, string name, string type)
    {

        BlobContainerClient container = new BlobContainerClient(blobStorageOptions.BlobUrl, blobStorageOptions.Container);
        string path = $"{type}/{name}-{DateTimeOffset.Now.ToUnixTimeSeconds()}";
        BlobClient blob = container.GetBlobClient(path);

        // Open the file and upload its data
        using (Stream stream = image.OpenReadStream())
        {
            await blob.UploadAsync(stream);
        }

        var uri = blob.Uri.AbsoluteUri;
        return uri;
    }

    public async Task DeleteImageFromBlobStorage(string imageUrl)
    {
        // Parse the Blob Storage URI to get container name and blob path
        Uri uri = new Uri(imageUrl);
        string containerName = uri.Segments[1];  // Assuming the container name is the second segment in the URI

        // Get blobPath
        int startIndex = containerName.Length + 1;
        int length = uri.PathAndQuery.Length - containerName.Length - 1;
        string blobPath = uri.PathAndQuery.Substring(startIndex, length);

        BlobContainerClient container = new BlobContainerClient(blobStorageOptions.BlobUrl, containerName);
        BlobClient blob = container.GetBlobClient(blobPath);

        // Check if the blob exists before attempting to delete
        if (await blob.ExistsAsync())
        {
            await blob.DeleteAsync();
        }
    }

    public async Task RestoreContainer(string imageUrl)
    {
        BlobContainerClient container = new BlobContainerClient(blobStorageOptions.BlobUrl, blobStorageOptions.Container);
        Uri uri = new Uri(imageUrl);
        string containerName = uri.Segments[1];  // Assuming the container name is the second segment in the URI

        // Get blobPath
        int startIndex = containerName.Length + 1;
        int length = uri.PathAndQuery.Length - containerName.Length - 1;
        string blobPath = uri.PathAndQuery.Substring(startIndex, length);

        // Kiểm tra xem blob có tồn tại trong container và đã bị xóa không
        try
        {
            var blobClient = container.GetBlobClient(blobPath);
            await blobClient.UndeleteAsync();
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            Console.WriteLine($"Blob '{blobPath}' was not found in container.");
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Lỗi khi kiểm tra trạng thái blob: {ex.Message}");
        }
    }
}
