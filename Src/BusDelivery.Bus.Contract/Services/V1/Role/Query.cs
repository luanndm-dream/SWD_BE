using BusDelivery.Contract.Abstractions.Message;

namespace BusDelivery.Contract.Services.V1.Role;
public static class Query
{
    public record GetRoleQuery() : IQuery<IReadOnlyCollection<Responses.RoleResponse>>;
    public record GetRoleByIdQuery(Guid Id) : IQuery<Responses.RoleResponse>;
}
