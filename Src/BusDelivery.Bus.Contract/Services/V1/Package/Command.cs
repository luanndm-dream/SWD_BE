using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace BusDelivery.Contract.Services.V1.Package;
public class Command
{
    public record CreatePackageCommand(
    int busId,
    int fromOfficeId,
    int toOfficeId,
    int stationId,
    int quantity,
    float totalWeight,
    float totalPrice,
    IFormFile image,
    string note,
    PackageStatus status,
    DateTime createTime) : ICommand<Responses.PackageResponse>;

    public record UpdatePackageCommand(
    int id,
    int busId,
    int fromOfficeId,
    int toOfficeId,
    int stationId,
    int quantity,
    float totalWeight,
    float totalPrice,
    IFormFile image,
    string note,
    PackageStatus status,
    DateTime createTime) : ICommand<Responses.PackageResponse>;

    public record DeletePackageCommand(int id) : ICommand;
}
