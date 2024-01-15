namespace BusDelivery.Infrastructure.BlobStorage.DependencyInjection.Options;
public class BlobStorageOptions
{
    public string blobUrl { get; set; }
    public string resourceGroup { get; set; }
    public string account { get; set; }
    public string container { get; set; }
    public string key { get; set; }
}
