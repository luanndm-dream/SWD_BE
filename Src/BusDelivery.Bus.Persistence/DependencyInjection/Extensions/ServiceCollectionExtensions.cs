using BusDelivery.Domain.Abstractions;
using BusDelivery.Domain.Abstractions.Repositories;
using BusDelivery.Persistence.DependencyInjection.Options;
using BusDelivery.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BusDelivery.Persistence.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptions<SqlServerRetryOptions>>();

            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(true) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("ConnectionStrings"),
                sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder.ExecutionStrategy(
                                dependencies => new SqlServerRetryingExecutionStrategy(
                                    dependencies: dependencies,
                                    maxRetryCount: options.Value.MaxRetryCount,
                                    maxRetryDelay: options.Value.MaxRetryDelay,
                                    errorNumbersToAdd: options.Value.ErrorNumbersToAdd))
                            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
        });
    }

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
        => services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork))
            .AddTransient<OfficeRepository>()
            .AddTransient<CoordinateRepository>()
            .AddTransient<UserRepository>()
            .AddTransient<RoleRepository>()
            .AddTransient<OrderRepository>()
            .AddTransient<PackageRepository>();


    public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<SqlServerRetryOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
