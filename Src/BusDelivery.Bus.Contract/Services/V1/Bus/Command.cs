using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;

namespace BusDelivery.Contract.Services.V1.Bus;
public class Command
{
    public class CreateBusCommandRequest: ICommand<Responses.BusResponse>
    {
        public string number { get; set; } = null!;
        public string plateNumber { get; set; } = null!;
        public string name { get; set; } = null!;
        public string organization { get; set; } = null!;
        public string color { get; set; } = null!;
        public string numberOfSeat { get; set; } = null!;
        public string operateTime { get; set; } = null!;   
        public bool IsActive { get; set; }
    }

    public record UpdateBusCommandRequest(int id) : ICommand<Responses.BusResponse>
    {
        // public int id { get; set; }
        public string number { get; set; } = null!;
        public string plateNumber { get; set; } = null!;
        public string name { get; set; } = null!;
        public string organization { get; set; } = null!;
        public string color { get; set; } = null!;
        public string numberOfSeat { get; set; } = null!;
        public string operateTime { get; set; } = null!;       
        public bool IsActive { get; set; }
    }

    public record DeleteBusCommandRequest( int id) : ICommand
    { }
    
}
