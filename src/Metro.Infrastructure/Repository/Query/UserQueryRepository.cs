using Dapper;
using Metro.Application.Contracts.Repositories.Query.Base;
using Metro.Core.Entities;
using Metro.Infrastructure.Configs;
using Metro.Infrastructure.Repository.Query.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.Commands.Users;

namespace Metro.Infrastructure.Repository.Query
{
    public class UserQueryRepository : MultipleResultQueryRepository<User>, IUserQueryRepository
    {
        public UserQueryRepository(IConfiguration configuration, IOptions<MetroSettings> settings) : base(configuration, settings)
        {
        }

        public async Task<User> GetByEmailAsync(AuthenticateUserCommand request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", request.Email);
            parameters.Add("@Password", request.Password);

            string query = "SELECT u.Id, u.UserName, " +
                           "u.Email, u.Password, u.Role " +
                           "FROM Users AS u WHERE Email = @Email " +
                           "AND Password = @Password AND IsDeleted = 0";

            return await SingleAsync(query, parameters);
        }
    }
}
