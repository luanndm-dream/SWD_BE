using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Route;
public class Command
{
    public record CreateRouteCommandRequest : ICommand<Responses.RouteResponse>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string StartPoint { get; set; } = null!;
        public string EndPoint { get; set; } = null!;
        public string OperateTime { get; set; } = null!;
    }
    public record UpdateRouteCommandRequest : ICommand<Responses.RouteResponse>
    {
        public int id { get; set; } 
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
        public string StartPoint { get; set; } = null!;
        public string EndPoint { get; set; } = null!;
        public string OperateTime { get; set; } = null!;
    }
    public record DeleteRouteCommandRequest(int id ) : ICommand;
}
