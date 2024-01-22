using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDelivery.Contract.Services.V1.Route;
 public class Responses
{
    public class RouteResponse
    {
        public int id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
        public string StartPoint { get; set; } = null!;
        public string EndPoint { get; set; } = null!;
        public string OperateTime { get; set; } = null!;
    }
}
