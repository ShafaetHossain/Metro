using Metro.Application.Contracts.Repositories.Command.Base;
using Metro.Application.Contracts.Repositories;
using Metro.Infrastructure.Configs;
using Metro.Infrastructure.Persistence;
using Metro.Infrastructure.Repository.Command.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Metro.Infrastructure.Repository;
using Metro.Application.Contracts.Repositories.Command;
using Metro.Infrastructure.Repository.Command;
using Metro.Application.Contracts.Repositories.Query;
using Metro.Infrastructure.Repository.Query;

namespace Metro.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MetroSettings>(configuration);
            var serviceProvider = services.BuildServiceProvider();
            var opt = serviceProvider.GetRequiredService<IOptions<MetroSettings>>().Value;

            //For SQL Server Connection
            services.AddDbContext<MetroDbContext>(options =>
            {
                options.UseSqlServer(
                    opt.ConnectionStrings.MetroDbConnection,
                    sqlServerOptionsAction: sqlOptions => { }
                    );
            });

            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<Func<MetroDbContext>>((provider) => provider.GetService<MetroDbContext>);
            services.AddScoped<DbFactory>();

            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStationQueryRepository, StationQueryRepository>();
            services.AddScoped<IStationCommandRepository, StationCommandRepository>();

            return services;
        }
    }
}