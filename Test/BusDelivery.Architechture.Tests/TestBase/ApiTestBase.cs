using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Identity.Client;
using Moq;
using AutoMapper;
using MediatR;
using BusDelivery.Persistence;
using BusDelivery.Application.Mapper;
using BusDelivery.Persistence.Repositories;

namespace BusDelivery.Architecture.Tests.TestBase;
public abstract class ApiTestBase
{
    protected readonly Mock<IMediator> _mockMediator;
    protected readonly Mock<ApplicationDbContext> _mockContext;
    //protected readonly Mock<RepositoryBase> _mockContext;\
    protected readonly Mock<StationRepository> _stationRepository;
    protected readonly IMapper _mapper;

    protected ApiTestBase()
    {
        _stationRepository = new Mock<StationRepository>();
        _mockContext = new Mock<ApplicationDbContext>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ServiceProfile());

        });
        _mapper = mapperConfig.CreateMapper();
        _mockMediator = new Mock<IMediator>();
    } 

}
