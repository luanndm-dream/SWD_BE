using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDelivery.Contract.Services.V1.Reports;
 public class Responses
{
    public record ReportResponse
    {
        public string Content { get; set; } = null!;
        public Guid CreateBy { get; set; }
        public int TargetId { get; set; }
        public string Type { get; set; } = null!;
        public string CreateTime { get; set; } = null!;
    }
}
