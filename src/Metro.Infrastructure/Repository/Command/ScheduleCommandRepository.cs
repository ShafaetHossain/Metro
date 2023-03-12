using Metro.Application.Contracts.Repositories.Command;
using Metro.Core.Entities;
using Metro.Infrastructure.Persistence;
using Metro.Infrastructure.Repository.Command.Base;

namespace Metro.Infrastructure.Repository.Command
{
    public class ScheduleCommandRepository : CommandRepository<Schedule>, IScheduleCommandRepository
    {
        public ScheduleCommandRepository(DbFactory dbFactory) : base(dbFactory) { }
    }
}
