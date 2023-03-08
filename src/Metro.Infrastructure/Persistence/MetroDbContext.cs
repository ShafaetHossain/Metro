using Metro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Metro.Infrastructure.Persistence
{
    public class MetroDbContext : DbContext
    {
        public MetroDbContext(DbContextOptions<MetroDbContext> options) : base(options) 
        {
        }

        #region Tables

        public DbSet<Station> Stations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TicketInfo> TicketInfo { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
