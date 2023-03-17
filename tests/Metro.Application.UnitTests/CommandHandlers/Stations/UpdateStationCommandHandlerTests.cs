using Metro.Application.Contracts.Repositories.Command;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Application.Contracts.Repositories;
using Moq;
using MediatR;
using Metro.Application.CommandHandlers.Stations;
using AutoMapper;
using Metro.Application.Common.Exceptions;
using System.Diagnostics.Metrics;
using Xunit;
using Metro.Core.Entities;
using Shared.Commands.Stations;
using Shared.DTOs;

namespace Metro.Application.UnitTests.CommandHandlers.Stations
{
    public class UpdateStationCommandHandlerTests
    {
        private readonly Mock<IStationCommandRepository> _stationCommandRepositoryMock;
        private readonly Mock<IStationQueryRepository> _stationQueryRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly UpdateStationCommandHandler _handler;

        public UpdateStationCommandHandlerTests()
        {
            _stationCommandRepositoryMock = new Mock<IStationCommandRepository>();
            _stationQueryRepositoryMock = new Mock<IStationQueryRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();

            _handler = new UpdateStationCommandHandler(_stationCommandRepositoryMock.Object, _stationQueryRepositoryMock.Object,
                _mediatorMock.Object, _mapperMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_SuccessMessage_If_SuccessfullyCommits()
        {
            //Arrange
            _stationQueryRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Station());
            _stationCommandRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Station>())).Returns(Task.FromResult(new Station()));

            //Act
            var result = await _handler.Handle(new UpdateStationCommand(), default);

            //Assert
            Assert.IsNotType<StationResponseDTO>(result);
        }

        [Fact]
        public async Task Handle_Should_Throw_NotFoundException_If_Country_EntityNotFound()
        {
            //Arrange
            _stationQueryRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Station)null);
            _stationCommandRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Station>())).Returns(Task.FromResult(new Station()));

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new UpdateStationCommand(), default));
        }
    }
}
