using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Authentication;
public static class Responses
{
    public record UserReponses(
        Guid roleId,
        int officeId,
        string name,
        string email,
        string password,
        string phoneNumber,
        Gentle gentle,
        string deviceId,
        string deviceVersion,
        string OS,
        bool IsDeleted,
        bool IsActive);

    public record LoginResponses(
        Guid userId,
        Guid roleId,
        int officeId,
        string name,
        string email,
        string phoneNumber,
        Gentle gentle,
        string deviceId,
        string deviceVersion,
        string OS,
        bool IsDeleted,
        bool IsActive,
        string Token,
        DateTime ExpireOn);
}
