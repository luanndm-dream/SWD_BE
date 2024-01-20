﻿using BusDelivery.Contract.Abstractions.Message;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Enumerations;

namespace BusDelivery.Contract.Services.V1.User;
public static class Query
{
    public record GetUserQuery(
        Guid? roleId,
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize)
        : IQuery<PagedResult<Responses.UserResponse>>;

    public record GetUserByIdQuery(Guid Id) : IQuery<Responses.UserResponse>;
}
