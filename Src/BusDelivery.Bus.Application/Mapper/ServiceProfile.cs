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
        CreateMap<Office, Contract.Services.V1.Office.Responses.OfficeReponses>().ReverseMap();
        CreateMap<PagedResult<Office>, PagedResult<Contract.Services.V1.Office.Responses.OfficeReponses>>().ReverseMap();
        CreateMap<Station, Contract.Services.V1.Station.Responses.GetStationResponse>().ReverseMap();
        CreateMap<PagedResult<Station>, PagedResult<Contract.Services.V1.Station.Responses.GetStationResponse>>().ReverseMap();
    }

}
