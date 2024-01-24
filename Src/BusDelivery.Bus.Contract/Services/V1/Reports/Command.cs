using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Reports;
public class Command
{
    public record CreateReportCommandRequest : ICommand<Responses.ReportResponse>
    {
        public string Content { get; set; } = null!;
        public Guid CreateBy { get; set; }
        public int TargetId { get; set; }
        public string Type { get; set; } = null!;
        public string CreateTime { get; set; } = null!;
    } 

    public record UpdateReportCommandRequest : ICommand<Responses.ReportResponse>
    {
        public int id {  get; set; }
        public string Content { get; set; } = null!;
        public Guid CreateBy { get; set; }
        public int TargetId { get; set; }
        public string Type { get; set; } = null!;
        public string CreateTime { get; set; } = null!;
    }
    public record DeleteReportCommandRequest : ICommand
    {
        public int id { get; set; } 
    }

}
