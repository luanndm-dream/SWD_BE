using AutoMapper;
using BusDelivery.Contract.Abstractions.Shared;
using BusDelivery.Contract.Services.V1.Office;
using BusDelivery.Domain.Entities;

namespace BusDelivery.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Office, Responses.OfficeReponses>().ReverseMap();
        CreateMap<PagedResult<Office>, PagedResult<Responses.OfficeReponses>>().ReverseMap();
    }
}
