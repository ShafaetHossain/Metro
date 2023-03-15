using FluentAssertions;
using MediatR;
using Metro.API.Controllers;
using Metro.API.UnitTests.Constants;
using Metro.Application.Queries.Schedules;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Commands.Schedules;
using Xunit;

namespace Metro.API.UnitTests.Controllers
{
    public class ScheduleControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ScheduleController _controller;

        public ScheduleControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ScheduleController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllSchedule_Should_Return_Success_Response()
        {
            // arrange
            var allScheduleQuery = new GetAllScheduleQuery();

            // act
            var result = (OkObjectResult)await _controller.GetAllSchedule(allScheduleQuery);

            // assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }

        [Fact]
        public async Task GetScheduleById_Should_Return_Success_Response()
        {
            // arrange
            var id = new Guid();

            // act
            var result = (OkObjectResult)await _controller.GetScheduleById(id);

            // assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }

        [Fact]
        public async Task CreateSchedule_Should_Return_Success_Response()
        {
            // arrange
            var scheduleCreateCommand = new CreateScheduleCommand();

            // act
            var result = (OkObjectResult)await _controller.CreateSchedule(scheduleCreateCommand);

            // assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }

        [Fact]
        public async Task UpdateSchedule_Should_Return_Success_Response()
        {
            // Arrange
            var id = new Guid();
            var scheduleUpdateCommand = new UpdateScheduleCommand();

            // Act
            var result = (OkObjectResult)await _controller.UpdateSchedule(id, scheduleUpdateCommand);

            // Assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }

        [Fact]
        public async Task DeleteSchedule_Should_Return_Success_Response()
        {
            // Arrange
            var guid = new Guid();

            // Act
            var result = (OkObjectResult)await _controller.DeleteSchedule(guid);

            // Assert
            result.StatusCode.Should().Be(HttpStatusConstants.StatusCode200);
        }
    }
}
