using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Package;
public class Responses
{
    public record PackageResponse(
    int Id,
    int BusId,
    int FromOfficeId,
    int ToOfficeId,
    int StationId,
    int Quantity,
    float TotalWeight,
    float TotalPrice,
    string Image,
    string Note,
    PackageStatus Status,
    string CreateTime);
}
