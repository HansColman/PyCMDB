﻿using CMDB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMDB.Infrastructure.Configurations
{
    public class AssetTypeConfiguration : IEntityTypeConfiguration<AssetType>
    {
        public void Configure(EntityTypeBuilder<AssetType> builder)
        {
            builder.ToTable(nameof(AssetType));

            builder.HasKey(e => e.TypeID)
                .HasName("PK_AssetType")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(e => e.Type)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.Vendor)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany(d => d.Types)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne(e => e.LastModifiedAdmin)
                .WithMany(p => p.AssetTypes)
                .HasForeignKey(e => e.LastModifiedAdminId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_AssetType_LastModifiedAdmin");

            builder.Property(e => e.active)
                .IsRequired()
                .HasMaxLength(1)
                .HasDefaultValue(1);

            builder.Property(e => e.DeactivateReason)
                .HasColumnType("varchar(255)");
        }
    }
}
