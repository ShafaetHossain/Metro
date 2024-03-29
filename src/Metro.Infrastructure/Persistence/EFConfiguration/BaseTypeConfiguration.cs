﻿using Metro.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metro.Infrastructure.Persistence.EFConfiguration
{
    public class BaseTypeConfiguration<TItem> : IEntityTypeConfiguration<TItem> where TItem : BaseEntity<Guid>
    {
        public virtual void Configure(EntityTypeBuilder<TItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedBy).HasMaxLength(100);
            builder.Property(x => x.LastModifiedBy).HasMaxLength(100);
        }
    }
}