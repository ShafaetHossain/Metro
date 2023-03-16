using AutoMapper;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Application.Queries.Stations;
using Metro.Core.Entities;
using Moq;
using Shared.DTOs;
using Xunit;

namespace Metro.Application.UnitTests.Queries.Stations
{
    public class GetAllStationQueryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IStationQueryRepository> _stationQueryRepositoryMock;

        public GetAllStationQueryTests()
        {
            // Initialize the AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Station, StationResponseDTO>();
            });

            _mapper = config.CreateMapper();

            // Initialize the mock repository
            _stationQueryRepositoryMock = new Mock<IStationQueryRepository>();
        }

        [Fact]
        public async Task Handle_Returns_PaginatedListResponseDTO_Of_StationResponseDTO()
        {
            // arrange
            var query = new GetAllStationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                StationName = "Uttara",
            };

            var stations = new List<Station>
            {
                new Station(),
                new Station(),
                new Station()
            };
            var count = 1L;

            _stationQueryRepositoryMock.Setup(repo => repo.GetAllAsync(query))
                .ReturnsAsync((count, stations));

            var handler = new GetAllStationQueryHandler(_stationQueryRepositoryMock.Object, _mapper);

            // act
            var result = await handler.Handle(query, CancellationToken.None);

            // assert
            Assert.NotNull(result);
            Assert.IsType<PaginatedListResponseDTO<StationResponseDTO>>(result);
        }
    }
}
