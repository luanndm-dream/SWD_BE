using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusDelivery.Application.Usecases.V1.Station.Queries;
using BusDelivery.Architecture.Tests.TestBase;
using BusDelivery.Contract.Services.V1.Station;
using BusDelivery.Domain.Entities;
using BusDelivery.Persistence;
using BusDelivery.Persistence.Repositories;
using MockQueryable.Moq;
using Moq;

namespace BusDelivery.Architecture.Tests;
public class StationTest : ApiTestBase
{
    private readonly List<Station> _sampleStation;
    private readonly StationRepository stationRepository;
    

    public StationTest()
    {
        _sampleStation = new List<Station>()
        {
            new()
            {
               Id = 1,
               OfficeId = 1,
               Name = "Test",
               Lat = "hehe",
               Lng = "hihi"
            }
        };
    }
    private void Setup()
    {

        //_mockContext.Setup(src => src.Stations).Returns(_sampleStation.AsQueryable().BuildMockDbSet().Object);
        var mockDbSet = _sampleStation.AsQueryable().BuildMockDbSet().Object;
        _mockContext.Setup(src => src.Set<Station>()).Returns(mockDbSet);
        
    }
    [Fact]
    public async Task ShouldReturn_CorrectStation()
    {
        Setup();
        //_stationRepository.Setups(x => x.GetAll()).Returns(_sampleStation);
        var handler = new GetStationByIdHandler(stationRepository, _mapper);
        var request = new Query.GetStationById(1);

        var result = await handler.Handle(request, default);
        Assert.NotNull(result);
        Assert.IsType<Responses.GetStationResponse>(result);
    }
}
