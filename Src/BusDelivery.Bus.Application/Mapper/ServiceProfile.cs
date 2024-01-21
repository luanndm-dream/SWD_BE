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
        CreateMap<Office, Responses.OfficeReponses>().ReverseMap();
        CreateMap<PagedResult<Office>, PagedResult<Responses.OfficeReponses>>().ReverseMap();

        // User
        CreateMap<User, BusDelivery.Contract.Services.V1.Authentication.Responses.UserReponses>().ReverseMap();

        // Coordinate
        CreateMap<Coordinate, BusDelivery.Contract.Services.V1.Coordinate.Responses.CoordinateResponses>().ReverseMap();
        CreateMap<PagedResult<Coordinate>, PagedResult<BusDelivery.Contract.Services.V1.Coordinate.Responses.CoordinateResponses>>().ReverseMap();

        // Order
        CreateMap<Order, BusDelivery.Contract.Services.V1.Order.Responses.OrderResponses>().ReverseMap();
        CreateMap<PagedResult<Order>, PagedResult<BusDelivery.Contract.Services.V1.Order.Responses.OrderResponses>>().ReverseMap();

    }
}
