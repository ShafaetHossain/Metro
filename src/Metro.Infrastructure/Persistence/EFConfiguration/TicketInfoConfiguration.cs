using Metro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metro.Infrastructure.Persistence.EFConfiguration
{
    public class TicketInfoConfiguration : BaseTypeConfiguration<TicketInfo>
    {
        public override void Configure(EntityTypeBuilder<TicketInfo> builder)
        {
            base.Configure(builder);

            //set table name
            builder.ToTable("TicketInfo");

            builder.HasOne<Schedule>(s => s.Schedule)
                .WithMany()
                .HasForeignKey(s => s.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.ScheduleId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}