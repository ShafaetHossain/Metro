using Metro.Application.Contracts.Repositories.Command;
using Metro.Core.Entities;
using Metro.Infrastructure.Persistence;

namespace Metro.Infrastructure.Repository.Command
{
    public class StationCommandRepository : CommandRepository<Station>, IStationCommandRepository
    {
        public StationCommandRepository(DbFactory dbFactory) : base(dbFactory) { }
    }
}
