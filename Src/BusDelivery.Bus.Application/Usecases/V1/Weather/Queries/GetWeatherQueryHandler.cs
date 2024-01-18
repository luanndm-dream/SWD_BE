using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Weather;

namespace BusDelivery.Application.Usecases.V1.Weather.Queries;
public sealed class GetWeatherQueryHandler : IQueryHandler<Query.GetWeatherQuery, IReadOnlyCollection<Responses.GetWeatherResponse>>
{
    public Task<Result<IReadOnlyCollection<Responses.GetWeatherResponse>>> Handle(Query.GetWeatherQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
