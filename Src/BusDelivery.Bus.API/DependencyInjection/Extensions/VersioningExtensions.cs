using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

namespace BusDelivery.API.DependencyInjection.Extensions;

public static class VersioningExtensions
{
    public static void AddVersioningConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSwaggerGenNewtonsoftSupport()
            .AddFluentValidationRulesToSwagger()
            .AddEndpointsApiExplorer()
            .AddSwagger(configuration);
        services
            .AddApiVersioning(options => options.ReportApiVersions = true)
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}
