using BusDelivery.Contract.Enumerations;
using static BusDelivery.Contract.Services.V1.Reports.Responses;

namespace BusDelivery.Contract.Services.V1.Package;
public class Responses
{
    public record PackageResponse()
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public int FromOfficeId { get; set; }
        public int ToOfficeId { get; set; }
        public int StationId { get; set; }
        public int Quantity { get; set; }
        public float TotalWeight { get; set; }
        public float TotalPrice { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        public PackageStatus Status { get; set; }
        public string CreateTime { get; set; }
        public ReportResponse Report { get; set; }
    }

}
