using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Bus;
public class Query
{
    public record GetBus(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex, int pageSize)
        : IQuery<PagedResult<Responses.BusResponse>>;

    public record GetBusById (int id): IQuery<Responses.BusResponse>
    {
        //public int Id { get; set; }
    }
}
