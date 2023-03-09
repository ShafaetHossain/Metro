using Metro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metro.Infrastructure.Persistence.EFConfiguration
{
    public class ScheduleConfiguration : BaseTypeConfiguration<Schedule>
    {
        public override void Configure(EntityTypeBuilder<Schedule> builder)
        {
            base.Configure(builder);

            //set table name
            builder.ToTable("Schedules");

            builder.Property(x => x.StationFromId)
                .IsRequired();
            builder.Property(x => x.StationToId)
                .IsRequired();
            builder.Property(x => x.DepartureTime)
                .IsRequired();
            builder.Property(x => x.TotalSeat)
                .IsRequired();
            builder.Property(x => x.Price)
                .IsRequired();
        }
    }
}