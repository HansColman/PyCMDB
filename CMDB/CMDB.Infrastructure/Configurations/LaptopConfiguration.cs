﻿using CMDB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMDB.Infrastructure.Configuration
{
    public class LaptopConfiguraton : IEntityTypeConfiguration<Laptop>
    {
        public void Configure(EntityTypeBuilder<Laptop> builder)
        {
            builder.HasBaseType<Device>();

            builder.Property(e => e.MAC)
                .HasColumnType("varchar(255)");

            builder.Property(e => e.RAM)
                .HasColumnType("varchar(255)");
        }
    }
}
