using AutoMapper;
using Azure;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Contract.Services.V1.Station;
using BusDelivery.Domain.Entities;

namespace BusDelivery.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // Station
        CreateMap<Station, Contract.Services.V1.Station.Responses.GetStationResponse>().ReverseMap();
        CreateMap<PagedResult<Station>, PagedResult<Contract.Services.V1.Station.Responses.GetStationResponse>>().ReverseMap();
        // Bus
        CreateMap<Bus, Contract.Services.V1.Bus.Responses.BusResponse>().ReverseMap();
        CreateMap<PagedResult<Bus>, PagedResult<Contract.Services.V1.Bus.Responses.BusResponse>>().ReverseMap();
        // Office
        CreateMap<Office, Contract.Services.V1.Office.Responses.OfficeResponse>().ReverseMap();
        CreateMap<PagedResult<Office>, PagedResult<Contract.Services.V1.Office.Responses.OfficeResponse>>().ReverseMap();

        // User
        CreateMap<User, BusDelivery.Contract.Services.V1.User.Responses.UserResponse>().ReverseMap();
        CreateMap<PagedResult<User>, PagedResult<BusDelivery.Contract.Services.V1.User.Responses.UserResponse>>().ReverseMap();

        // Weather
        CreateMap<Weather, BusDelivery.Contract.Services.V1.Weather.Responses.GetWeatherResponse>().ReverseMap();

        // Role
        CreateMap<IReadOnlyCollection<Role>, IReadOnlyCollection<BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>>().ReverseMap();

        // Report
        CreateMap<Report, Contract.Services.V1.Reports.Responses.ReportResponse>().ReverseMap();
        CreateMap<PagedResult<Report>, PagedResult<Contract.Services.V1.Reports.Responses.ReportResponse>>().ReverseMap();  
        
        //Route
        CreateMap<Route, Contract.Services.V1.Route.Responses.RouteResponse>().ReverseMap();
        CreateMap<PagedResult<Route>, PagedResult<Contract.Services.V1.Route.Responses.RouteResponse>>().ReverseMap();
        CreateMap<PagedResult<Role>, PagedResult<BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>>().ReverseMap();
        CreateMap<Role, BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>().ReverseMap();
    }

}
