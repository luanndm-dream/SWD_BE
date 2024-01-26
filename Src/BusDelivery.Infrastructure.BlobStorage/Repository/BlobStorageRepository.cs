using System.Drawing;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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

    public async Task<string?> GetResizeImageToBase64(string imageUrl, int width, int height)
    {
        var blobPath = GetBlobPath(imageUrl);
        var blobClient = GetBlobClient(blobPath);

        if (!await BlobExists(blobClient))
        {
            return null;
        }

        var base64String = await GetResizedImageBase64(blobClient, width, height);

        return base64String;
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

    private BlobClient GetBlobClient(string blobPath)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageOptions.BlobUrl);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageOptions.Container);
        return blobContainerClient.GetBlobClient(blobPath);
    }

    private async Task<bool> BlobExists(BlobClient blobClient)
    {
        try
        {
            return await blobClient.ExistsAsync();
        }
        catch
        {
            return false;
        }
    }

    private async Task<string> GetResizedImageBase64(BlobClient blobClient, int width, int height)
    {
        using (var memoryStream = new MemoryStream())
        {
            var download = await blobClient.DownloadAsync();
            await download.Value.Content.CopyToAsync(memoryStream);

            using (var originalImage = Image.FromStream(memoryStream))
            using (var resizedImage = new Bitmap(originalImage, width, height))
            using (var memoryStreamResized = new MemoryStream())
            {
                resizedImage.Save(memoryStreamResized, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = memoryStreamResized.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
