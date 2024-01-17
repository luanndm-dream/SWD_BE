using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Station;
public class Command
{
    public record CreateStationRequest : ICommand<Responses.GetStationResponse>
    {
        public int officeId {  get; set; }
        public string? name { get; set; }
        public string? lat { get; set; }
        public string? lng { get; set; }
    }
    public record UpdateStationRequest(int stationId) : ICommand<Responses.GetStationResponse>
    {
        //public int id { get; set; }
        public int officeId { get; set; }
        public string? name { get; set; }
        public string? lat { get; set; }
        public string? lng { get; set; }
    }

    public record DeleteStationRequest(int stationID) : ICommand;
   
}
