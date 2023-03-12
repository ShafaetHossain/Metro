using Metro.Application.Contracts.Repositories.Query.Base;
using Metro.Application.Queries.Schedules;
using Metro.Core.Entities;

namespace Metro.Application.Contracts.Repositories.Query
{
    public interface IScheduleQueryRepository : IMultipleResultQueryRepository<Schedule>
    {
        Task<(long, IEnumerable<Schedule>)> GetAllAsync(GetAllScheduleQuery request);
    }
}
