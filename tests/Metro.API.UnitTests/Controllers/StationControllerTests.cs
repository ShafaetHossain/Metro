using FluentAssertions;
using MediatR;
using Metro.API.Controllers;
using Metro.API.UnitTests.Constants;
using Metro.Application.Queries.Stations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Commands.Stations;
using Xunit;

namespace Metro.API.UnitTests.Controllers
{
    public class StationControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly StationController _controller;

        public StationControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new StationController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllStation_Should_Return_Success_Response()
        {
            // arrange
            var allStationQuery = new GetAllStationQuery();

            // act
            var result = (OkObjectResult) await _controller.GetAllStation(allStationQuery);

            // assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }

        [Fact]
        public async Task GetStationById_Should_Return_Success_Response()
        {
            // arrange
            var id = new Guid();

            // act
            var result = (OkObjectResult)await _controller.GetStationById(id);

            // assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }

        [Fact]
        public async Task CreateStation_Should_Return_Success_Response()
        {
            // arrange
            var stationCreateCommand = new CreateStationCommand();

            // act
            var result = (OkObjectResult)await _controller.CreateStation(stationCreateCommand);

            // assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }

        [Fact]
        public async Task UpdateStation_Should_Return_Success_Response()
        {
            // Arrange
            var id = new Guid();
            var stationUpdateCommand = new UpdateStationCommand();

            // Act
            var result = (OkObjectResult)await _controller.UpdateStation(id, stationUpdateCommand);

            // Assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }

        [Fact]
        public async Task DeleteStation_Should_Return_Success_Response()
        {
            // Arrange
            var guid = new Guid();

            // Act
            var result = (OkObjectResult)await _controller.DeleteStation(guid);

            // Assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }
    }
}
