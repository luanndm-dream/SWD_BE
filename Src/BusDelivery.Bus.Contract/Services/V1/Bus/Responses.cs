using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDelivery.Contract.Services.V1.Bus;
public class Responses
{
    public class BusResponse
    {
        public int id {  get; init; }
        public string number { get; init; } = null!;
        public string plateNumber { get; init; } = null!;
        public string name { get; init; } = null!;
        public string organization { get; init; } = null!;  
        public string color { get; init; } = null!;
        public string numberOfSeat { get; init; } = null!;
        public string operateTime { get; init; } = null!;
        public bool IsActive { get; init; }
    }
}
