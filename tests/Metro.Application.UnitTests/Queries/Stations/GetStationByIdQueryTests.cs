using AutoMapper;
using FluentAssertions;
using Metro.Application.Common.Mappings;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Application.Queries.Stations;
using Metro.Common.UnitTests.Mocks;
using Moq;
using Shared.DTOs;
using Xunit;

namespace Metro.Application.UnitTests.Queries.Stations
{
    public class GetStationByIdQueryTests
    {
        private readonly Mock<IStationQueryRepository> _stationQueryRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetStationByIdQueryHandler _handler;

        public GetStationByIdQueryTests()
        {
            _stationQueryRepositoryMock = StationMockRepository.GetStationByIdQueryRepository();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<StationMappingProfile>()).CreateMapper();
            _handler = new GetStationByIdQueryHandler(_stationQueryRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Will_Return_NotNull_When_Response_Found()
        {
            Guid id = new Guid("40979b45-9ab6-40be-9a4d-7aa01d226d27");
            GetStationByIdQuery response = new GetStationByIdQuery(id);
            var result = await _handler.Handle(response, CancellationToken.None);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Will_Return_True_When_Invalid_Type_IsNotMatched()
        {
            Guid id = new Guid("40979b45-9ab6-40be-9a4d-7aa01d226d27");
            GetStationByIdQuery response = new GetStationByIdQuery(id);
            var result = await _handler.Handle(response, CancellationToken.None);
            result.Should().BeOfType<StationResponseDTO>();
        }
    }
}
