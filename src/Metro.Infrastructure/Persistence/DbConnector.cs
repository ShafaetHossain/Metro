using Metro.Infrastructure.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Metro.Infrastructure.Persistence
{
    public class DbConnector
    {
        private readonly IConfiguration _configuration;
        private readonly MetroSettings _settings;
        public DbConnector(IConfiguration configuration, IOptions<MetroSettings> settings)
        {
            _configuration = configuration;
            _settings = settings.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _settings.ConnectionStrings.MetroDbConnection;
            return new SqlConnection(connectionString); //System.Data.SqlClient (NuGet)
        }
    }
}
