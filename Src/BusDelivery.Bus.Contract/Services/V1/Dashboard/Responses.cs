namespace BusDelivery.Contract.Services.V1.Dashboard;
public static class Responses
{
    public record DashboardResponses(
        int TotalUser,
        int NewUserInThisMonth,
        int TotalUserLastMonth,
        int TotalOrder,
        int NewOrderInThisMonth,
        int TotalOrderLastMonth,
        int TotalOffice,
        float TotalPriceInThisMonth
        );
}
