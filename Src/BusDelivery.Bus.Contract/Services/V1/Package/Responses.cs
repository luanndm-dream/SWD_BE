using BusDelivery.Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace BusDelivery.Contract.Services.V1.Package;
public class Responses
{
    public record PackageResponse(
    Guid Id,
    int BusId,
    int FromOfficeId,
    int ToOfficeId,
    int StationId,
    int Quantity,
    float TotalWeight,
    float TotalPrice,
    IFormFile Image,
    string Note,
    PackageStatus Status,
    string CreateTime);
}
