using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Route;
public class Query
{
    public record GetRouteById(int routeId): IQuery<Responses.RouteResponse>;
    
    public record GetRoute(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex, int pageSize)
        : IQuery<PagedResult<Responses.RouteResponse>>;
}
