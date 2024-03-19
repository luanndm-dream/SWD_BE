namespace BusDelivery.Contract.Services.V1.Reports;
public class Responses
{
    public record ReportResponse
    {
        public int id { get; set; }
        public string Content { get; set; } = null!;
        public int CreateBy { get; set; }
        public int TargetId { get; set; }
        public string Type { get; set; } = null!;
        public string CreateTime { get; set; } = null!;
    }
    public record CountReport
    {
        public int number { get; set; }
    }
}
