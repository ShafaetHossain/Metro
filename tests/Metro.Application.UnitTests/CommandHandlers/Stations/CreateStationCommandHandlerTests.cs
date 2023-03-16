using AutoMapper;
using Metro.Application.CommandHandlers.Stations;
using Metro.Application.Common.Mappings;
using Metro.Application.Contracts.Repositories;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Common.UnitTests.Mocks;
using Moq;
using Shared.Commands.Stations;
using Xunit;

namespace Metro.Application.UnitTests.CommandHandlers.Stations
{
    public class CreateStationCommandHandlerTests
    {
        private readonly Mock<IStationCommandRepository> _repositoryMock;
        private readonly CreateStationCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStationCommandHandlerTests() 
        {
            // arrange repo from mock
            _repositoryMock = StationMockRepository.GetStationCommandRepository();
            _unitOfWork = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<StationMappingProfile>());
            _mapper = config.CreateMapper();

            _handler = new CreateStationCommandHandler(_mapper, _repositoryMock.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task ValidateHandler()
        {
            var command = new CreateStationCommand
            {
                StationName = "Pallabi"
            };

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.NotNull(result);
            Assert.Equal(command.StationName, result.StationName);
        }
    }
}
