using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.Role;
public static class Query
{
    public record GetRoleQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.RoleResponse>>;
    public record GetRoleByIdQuery(Guid Id) : IQuery<Responses.RoleResponse>;
}
