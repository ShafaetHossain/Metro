using Metro.Core.Entities;
using Shared.Commands.Users;

namespace Metro.Application.Contracts.Repositories.Query.Base
{
    public interface IUserQueryRepository : IMultipleResultQueryRepository<User>
    {
        Task<User> GetByEmailAsync(AuthenticateUserCommand request);
    }
}
