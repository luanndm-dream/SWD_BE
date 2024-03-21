using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Dashboard;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Application.Usecases.V1.Dashboard.Queries;
public class GetDashboardQueryHandler : IQueryHandler<Query.GetDashboardQuery, Responses.DashboardResponses>
{
    private readonly UserRepository userRepository;
    private readonly OrderRepository orderRepository;
    private readonly OfficeRepository officeRepository;
    private readonly PackageRepository packageRepository;

    public GetDashboardQueryHandler(UserRepository userRepository, OrderRepository orderRepository, OfficeRepository officeRepository, PackageRepository packageRepository)
    {
        this.userRepository = userRepository;
        this.orderRepository = orderRepository;
        this.officeRepository = officeRepository;
        this.packageRepository = packageRepository;
    }


    public async Task<Result<Responses.DashboardResponses>> Handle(Query.GetDashboardQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // User
            var totalUser = await userRepository.Count();
            var newUserInThisMonth = await userRepository.NewUserInThisMonth();
            var totalUserLastMonth = await userRepository.TotalUserLastMonth();

            // Order
            var totalOrder = await packageRepository.TotalOrder();
            var newOrderInThisMonth = await packageRepository.NewOrderInThisMonth();
            var totalOrderLastMonth = await packageRepository.TotalOrderLastMonth();

            // GetTotalOffice
            var totalOffice = officeRepository.Count();

            // totalPriceThisMonth
            var totalPriceThisMonth = await packageRepository.TotalPriceThisMonth();

            var orderChart = await packageRepository.GetChart();

            var response = new Responses.DashboardResponses(
                totalUser,
                newUserInThisMonth,
                totalUserLastMonth,

                totalOrder,
                newOrderInThisMonth,
                totalOrderLastMonth,

                totalOffice,
                totalPriceThisMonth,

                orderChart
                );

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
