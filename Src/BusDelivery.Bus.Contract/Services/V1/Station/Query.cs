using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;
using BusDelivery.Contract.Services.V1.Office;

namespace BusDelivery.Contract.Services.V1.Station;
public class Query
{
    public record GetStation(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex, int pageSize)
        : IQuery<PagedResult<Responses.GetStationResponse>>;

    public record GetStationById(int stationId) : IQuery<Responses.GetStationResponse>;
}
