using AutoMapper;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Domain.Entities;

namespace BusDelivery.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // Office
        CreateMap<Office, Responses.OfficeResponse>().ReverseMap();
        CreateMap<PagedResult<Office>, PagedResult<Responses.OfficeResponse>>().ReverseMap();

        // User
        CreateMap<User, BusDelivery.Contract.Services.V1.User.Responses.UserResponse>().ReverseMap();
        CreateMap<PagedResult<User>, PagedResult<BusDelivery.Contract.Services.V1.User.Responses.UserResponse>>().ReverseMap();

        // Weather
        CreateMap<Weather, BusDelivery.Contract.Services.V1.Weather.Responses.GetWeatherResponse>().ReverseMap();

        // Coordinate
        CreateMap<Coordinate, BusDelivery.Contract.Services.V1.Coordinate.Responses.CoordinateResponses>().ReverseMap();
        CreateMap<PagedResult<Coordinate>, PagedResult<BusDelivery.Contract.Services.V1.Coordinate.Responses.CoordinateResponses>>().ReverseMap();

        // Order
        CreateMap<Order, BusDelivery.Contract.Services.V1.Order.Responses.OrderResponses>().ReverseMap();
        CreateMap<PagedResult<Order>, PagedResult<BusDelivery.Contract.Services.V1.Order.Responses.OrderResponses>>().ReverseMap();
        // Role

        CreateMap<PagedResult<Role>, PagedResult<BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>>().ReverseMap();
        CreateMap<Role, BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>().ReverseMap();

    }
}
