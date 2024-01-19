using BusDelivery.Domain.Entities;

namespace BusDelivery.Persistence.Repositories;
public class WeatherRepository : RepositoryBase<Weather, int>
{
    public WeatherRepository(ApplicationDbContext context) : base(context)
    {
    }


}
