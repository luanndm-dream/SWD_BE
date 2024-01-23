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

        // Role
        CreateMap<PagedResult<Role>, PagedResult<BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>>().ReverseMap();
        CreateMap<Role, BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>().ReverseMap();
    }
}
