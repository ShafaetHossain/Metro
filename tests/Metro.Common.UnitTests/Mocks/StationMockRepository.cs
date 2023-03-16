using Metro.Application.Contracts.Repositories.Command;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Core.Entities;
using Moq;

namespace Metro.Common.UnitTests.Mocks
{
    public static class StationMockRepository
    {
        private static List<Station> Stations = new()
        {
            new Station{
                Id = Guid.NewGuid(),
                StationName = "Uttara"
            },
            new Station{
                Id = Guid.NewGuid(),
                StationName = "Pallabi"
            },
        };

        private static Station Station = new Station
        {
            Id = Guid.NewGuid(),
            StationName = "Mirpur-10"
        };

        public static Mock<IStationCommandRepository> GetStationCommandRepository()
        {
            var repositoryMock = new Mock<IStationCommandRepository>();

            repositoryMock.Setup(x => x.InsertAsync(It.IsAny<Station>())).ReturnsAsync(
                (Station station) =>
                {
                    Stations.Add(station);
                    return station;
                });
            return repositoryMock;
        }

        public static Mock<IStationQueryRepository> GetStationByIdQueryRepository()
        {
            Guid id = Guid.NewGuid();
            var repositoryMock = new Mock<IStationQueryRepository>();
            repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(Station);
            return repositoryMock;
        }
    }
}
