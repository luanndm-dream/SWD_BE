using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusDelivery.Persistence.Repositories;
public class RouteRepository : RepositoryBase<Route, int>
{
    private readonly ApplicationDbContext context;
    public RouteRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<List<Station>> GetStationByRouteId(int routeId)
    {
        var sql = @"
        SELECT s.*
        FROM Station s
        JOIN StationRoute sr ON s.Id = sr.StationId
        WHERE sr.RouteId = {0}";

        var stations = context.Station
            .FromSqlRaw(sql, routeId)
            .ToList();

        return stations;
    }
}

