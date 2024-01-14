using BusDelivery.Infrastructure.BlobStorage.DependencyInjection.Options;
using BusDelivery.Infrastructure.BlobStorage.Repositories;
using BusDelivery.Infrastructure.BlobStorage.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BusDelivery.Infrastructure.BlobStorage.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigInfrastructureBlobStorage(this IServiceCollection services)
        => services.AddTransient(typeof(IBlobStorageRepository), typeof(BlobStorageRepository));
    public static OptionsBuilder<BlobStorageOptions> ConfigureBlobStorageOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<BlobStorageOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
