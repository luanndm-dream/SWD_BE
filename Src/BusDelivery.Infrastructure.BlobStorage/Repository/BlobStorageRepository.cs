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

    public string SaveImageOnBlobStorage(IFormFile image, string name)
    {

        BlobContainerClient container = new BlobContainerClient(blobStorageOptions.resourceGroup, blobStorageOptions.container);
        string path = $"{name}-{DateTimeOffset.Now.ToUnixTimeSeconds()}";
        BlobClient blob = container.GetBlobClient(path);

        // Open the file and upload its data
        using (Stream stream = image.OpenReadStream())
        {
            blob.Upload(stream);
        }

        var uri = blob.Uri.AbsoluteUri;
        return uri;
    }

    public void DeleteImageFromBlobStorage(string imageUrl)
    {
        // Parse the Blob Storage URI to get container name and blob path
        Uri uri = new Uri(imageUrl);
        string containerName = uri.Segments[1];  // Assuming the container name is the second segment in the URI
        string blobPath = uri.PathAndQuery.TrimStart('/');

        BlobContainerClient container = new BlobContainerClient(blobStorageOptions.resourceGroup, containerName);
        BlobClient blob = container.GetBlobClient(blobPath);

        // Check if the blob exists before attempting to delete
        if (blob.Exists())
        {
            // Delete the blob
            blob.Delete();
        }
    }
}
