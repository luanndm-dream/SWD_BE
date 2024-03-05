using AutoMapper;
using BusDelivery.Contract.Abstractions.Shared;
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
        CreateMap<User, BusDelivery.Contract.Services.V1.User.Responses.UserResponse>()
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString("dd/MM/yyyy"))).ReverseMap();
        CreateMap<PagedResult<User>, PagedResult<BusDelivery.Contract.Services.V1.User.Responses.UserResponse>>().ReverseMap();

        // Package
        CreateMap<Package, BusDelivery.Contract.Services.V1.Package.Responses.PackageResponse>()
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString("dd/MM/yyyy"))).ReverseMap();
        CreateMap<PagedResult<Package>, PagedResult<Contract.Services.V1.Package.Responses.PackageResponse>>().ReverseMap();
        // Weather
        CreateMap<Weather, BusDelivery.Contract.Services.V1.Weather.Responses.GetWeatherResponse>().ReverseMap();

        // Coordinate
        CreateMap<Coordinate, BusDelivery.Contract.Services.V1.Coordinate.Responses.CoordinateResponses>().ReverseMap();
        CreateMap<PagedResult<Coordinate>, PagedResult<BusDelivery.Contract.Services.V1.Coordinate.Responses.CoordinateResponses>>().ReverseMap();

        // Order
        CreateMap<Order, BusDelivery.Contract.Services.V1.Order.Responses.OrderResponses>().ReverseMap();
        CreateMap<PagedResult<Order>, PagedResult<BusDelivery.Contract.Services.V1.Order.Responses.OrderResponses>>().ReverseMap();

        // Report
        CreateMap<Report, Contract.Services.V1.Reports.Responses.ReportResponse>().ReverseMap();
        CreateMap<PagedResult<Report>, PagedResult<Contract.Services.V1.Reports.Responses.ReportResponse>>().ReverseMap();

        //Route
        CreateMap<Route, Contract.Services.V1.Route.Responses.RouteResponse>().ReverseMap();
        CreateMap<PagedResult<Route>, PagedResult<Contract.Services.V1.Route.Responses.RouteResponse>>().ReverseMap();
        //Role
        CreateMap<PagedResult<Role>, PagedResult<BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>>().ReverseMap();
        CreateMap<Role, BusDelivery.Contract.Services.V1.Role.Responses.RoleResponse>().ReverseMap();

    }

}
