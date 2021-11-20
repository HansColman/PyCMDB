﻿using CMDB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace CMDB.Infrastructure.Configuration
{
    class IdentityConfiguration : IEntityTypeConfiguration<Identity>
    {
        public void Configure(EntityTypeBuilder<Identity> builder)
        {
            builder.ToTable(nameof(Identity));

            builder.HasKey(e => e.IdenId)
                .HasName("PK_Identity")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(e => e.Name);

            builder.Property(e => e.UserID);

            builder.HasOne(e => e.Type)
                .WithMany(d => d.Identities)
                .HasForeignKey(e => e.TypeId)
                .HasConstraintName("FK_Identity_Type")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Language)
                .WithMany(d => d.Identities)
                .HasConstraintName("FK_Identity_Language")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.EMail);

            builder.Property(e => e.Company);

            builder.Property(e => e.active)
                .IsRequired()
                .HasMaxLength(1)
                .HasDefaultValue(1);

            builder.Property(e => e.DeactivateReason)
                .HasColumnType("varchar(255)");
        }
    }
}
