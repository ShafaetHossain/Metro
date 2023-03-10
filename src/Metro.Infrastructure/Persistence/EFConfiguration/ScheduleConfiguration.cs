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

            builder.HasOne<Station>(s => s.StationFrom)
                .WithMany()
                .HasForeignKey(s => s.StationFromId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Station>(s => s.StationTo)
                .WithMany()
                .HasForeignKey(s => s.StationToId)
                .OnDelete(DeleteBehavior.Restrict);

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