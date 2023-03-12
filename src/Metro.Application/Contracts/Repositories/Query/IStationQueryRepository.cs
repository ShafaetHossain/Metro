using Metro.Application.Contracts.Repositories.Query.Base;
using Metro.Application.Queries.Stations;
using Metro.Core.Entities;

namespace Metro.Application.Contracts.Repositories.Query
{
    public interface IStationQueryRepository : IMultipleResultQueryRepository<Station>
    {
        Task<(long, IEnumerable<Station>)> GetAllAsync(GetAllStationQuery request);
        Task<Station> GetByIdAsync(Guid stationId);
    }
}
