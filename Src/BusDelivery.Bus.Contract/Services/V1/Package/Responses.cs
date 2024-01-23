using Microsoft.AspNetCore.Http;

namespace BusDelivery.Contract.Services.V1.Package;
public class Responses
{
    public record PackageResponse(
    Guid Id,
    int BusId,
    int OfficeId,
    int StationId,
    int Quantity,
    float TotalWeight,
    float TotalPrice,
    IFormFile Image,
    string Note,
    bool IsActive,
    string CreateTime);
}
