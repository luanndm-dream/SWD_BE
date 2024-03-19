namespace BusDelivery.Contract.Services.V1.Dashboard;
public static class Responses
{
    public record DashboardResponses(
        int TotalUser,
        int TotalOrder,
        int TotalOffice,
        float Revenue
        );
}
