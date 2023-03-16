using AutoMapper;
using MediatR;
using Metro.Application.CommandHandlers.Stations;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Application.Queries.Stations;
using Metro.Core.Entities;
using Moq;
using Shared.Commands.Stations;
using Shared.DTOs;
using System.Diagnostics.Metrics;
using Xunit;

namespace Metro.Application.UnitTests.CommandHandlers.Stations
{
    public class DeleteStationCommandHandlerTests
    {
        private readonly Mock<IStationCommandRepository> _stationCommandRepositoryMock;
        private readonly Mock<IStationQueryRepository> _stationQueryRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IMapper _mapper;

        private readonly DeleteStationCommandHandler _handler;

        public DeleteStationCommandHandlerTests()
        {
            // Arrange repository from mock
            _stationCommandRepositoryMock = new Mock<IStationCommandRepository>();
            _stationQueryRepositoryMock = new Mock<IStationQueryRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mediatorMock = new Mock<IMediator>();

            // Create a mapper configuration for the StationResponseDTO to Station mapping
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StationResponseDTO, Station>();
            });

            // Create a mapper instance using the configuration
            _mapper = mapperConfig.CreateMapper();

            _handler = new DeleteStationCommandHandler(_stationCommandRepositoryMock.Object, _stationQueryRepositoryMock.Object , _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Will_Return_True_When_Handler_Return_String()
        {
            _stationQueryRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Station());
            _stationCommandRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Station>())).Returns(Task.FromResult(new Station()));

            // act
            var result = await _handler.Handle(new DeleteStationCommand(Guid.NewGuid()), CancellationToken.None);

            //Result
            Assert.Equal("Station information is deleted!", result);
        }
    }
}
