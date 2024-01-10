using BusDelivery.API.DependencyInjection.Extensions;
using BusDelivery.API.Middleware;
using BusDelivery.Persistence.DependencyInjection.Extensions;
using BusDelivery.Persistence.DependencyInjection.Options;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
// Application
//builder.Services.AddConfigureMediatR();
//builder.Services.AddConfigurationMapper();

// Infrastructure.Dapper
//builder.Services.AddInfrastructureDapper();
//builder.Services.AddHttpClient();
//builder.Services.AddHttpContextAccessor();

// Persistence
builder.Services.AddSqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.ConfigureSqlServerRetryOptions(
    builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
//builder.Services.ConfigureBlobStorageOptions(
//    builder.Configuration.GetSection(nameof(BlobStorageOptions)));
//builder.Services.ConfigureOpenWeatherMapOptions(
//    builder.Configuration.GetSection(nameof(OpenWeatherMapOptions)));

// Add Serilog
Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging
    .ClearProviders()
    .AddSerilog();
builder.Host.UseSerilog();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// API
builder.Services.AddControllers()
    .AddApplicationPart(BusDelivery.Presentation.AssemblyReference.Assembly);

// Versioning
builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddEndpointsApiExplorer()
    .AddSwagger();
builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.ConfigureSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}
