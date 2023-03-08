using Metro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metro.Infrastructure.Persistence.EFConfiguration
{
    public class StationConfiguration : BaseTypeConfiguration<Station>
    {
        public override void Configure(EntityTypeBuilder<Station> builder)
        {
            base.Configure(builder);

            //set table name
            builder.ToTable("Stations");

            builder.Property(x => x.StationName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
