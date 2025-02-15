using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartX.DAL.Configurations;

public class BrandConfigurations : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasOne(x => x.Supplier)
            .WithMany(x => x.Brands)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(25);

        builder.Property(x => x.Description)
           .IsRequired()
           .HasMaxLength(50);
    }
}
