using Metro.Application.Contracts.Repositories.Command;
using Metro.Core.Entities;
using Metro.Infrastructure.Persistence;
using Metro.Infrastructure.Repository.Command.Base;

namespace Metro.Infrastructure.Repository.Command
{
    public class UserCommandRepository : CommandRepository<User>, IUserCommandRepository
    {
        public UserCommandRepository(DbFactory dbFactory) : base(dbFactory) { }
    }
}
