using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BusDelivery.Infrastructure.BlobStorage.DependencyInjection.Options;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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
        Uri uri = new Uri(imageUrl);
        string containerName = uri.Segments[1];

        var blobPath = GetBlobPath(imageUrl);

        BlobContainerClient container = new BlobContainerClient(blobStorageOptions.BlobUrl, containerName);
        BlobClient blob = container.GetBlobClient(blobPath);

        // Check if the blob exists before attempting to delete
        if (await blob.ExistsAsync())
        {
            await blob.DeleteAsync();
        }
    }

    public async Task<string?> GetImageToBase64(string imageUrl)
    {
        var blobPath = GetBlobPath(imageUrl);
        var blobServiceClient = new BlobServiceClient(blobStorageOptions.BlobUrl);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageOptions.Container);

        var blobClient = blobContainerClient.GetBlobClient(blobPath);

        if (!await blobClient.ExistsAsync())
        {
            return null;
        }


        using (var memoryStream = new MemoryStream())
        {

            BlobDownloadInfo download = blobClient.Download();
            download.Content.CopyTo(memoryStream);
            if (memoryStream.Length > 0)
                return Convert.ToBase64String(memoryStream.ToArray());
        }
        return null;
    }

    public async Task<string?> GetImageToBase64(string imageUrl, int? width, int? height)
    {
        var blobPath = GetBlobPath(imageUrl);
        var blobServiceClient = new BlobServiceClient(blobStorageOptions.BlobUrl);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageOptions.Container);

        var blobClient = blobContainerClient.GetBlobClient(blobPath);


        using (var memoryStream = new MemoryStream())
        {

            BlobDownloadInfo download = blobClient.Download();
            download.Content.CopyTo(memoryStream);
            memoryStream.Position = 0;
            if (width.HasValue && height.HasValue)
            {
                using var image = await Image.LoadAsync(memoryStream);
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width.Value, height.Value),
                    Mode = ResizeMode.Max
                }));

                // Return the resized image
                memoryStream.Position = 0;
            }

            if (memoryStream.Length > 0)
                return Convert.ToBase64String(memoryStream.ToArray());
        }
        return null;
    }

    private string GetBlobPath(string imageUrl)
    {
        Uri uri = new Uri(imageUrl);
        string containerName = uri.Segments[1];  // Assuming the container name is the second segment in the URI

        // Get blobPath
        int startIndex = containerName.Length + 1;
        int length = uri.PathAndQuery.Length - containerName.Length - 1;
        string blobPath = uri.PathAndQuery.Substring(startIndex, length);
        return blobPath;
    }
}
