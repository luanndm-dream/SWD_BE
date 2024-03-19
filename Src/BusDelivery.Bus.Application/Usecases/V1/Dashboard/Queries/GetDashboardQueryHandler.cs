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
            // GetTotalUser In ThisMonth
            var totalUser = await userRepository.TotalUserInThisMonth();

            // GetTotalOrder In ThisMonth
            var totalOrder = await packageRepository.TotalOrderInThisMonth();

            // GetTotalOffice
            var totalOffice = officeRepository.Count();

            // GetRevenue
            var revenue = await packageRepository.RevenueInThisMonth();

            var response = new Responses.DashboardResponses(
                totalUser,
                totalOrder,
                totalOffice,
                revenue);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
