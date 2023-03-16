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
            Id = new Guid("40979b45-9ab6-40be-9a4d-7aa01d226d27"),
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
            Guid id = new Guid("40979b45-9ab6-40be-9a4d-7aa01d226d27");
            var repositoryMock = new Mock<IStationQueryRepository>();
            repositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(Station);
            return repositoryMock;
        }
    }
}