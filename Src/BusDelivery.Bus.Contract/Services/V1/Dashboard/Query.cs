using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Dashboard;
public class Query
{
    public record GetDashboardQuery() : IQuery<Responses.DashboardResponses>;
}
