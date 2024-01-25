using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.User;
public static class Responses
{
    public record UserResponse
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? RoleDescription { get; set; }
        public int OfficeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Identity { get; set; }
        public Gentle Gentle { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceVersion { get; set; }
        public OS? OS { get; set; }
        public string CreateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
