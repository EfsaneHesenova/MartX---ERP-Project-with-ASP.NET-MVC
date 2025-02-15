using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartX.DAL.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(x => x.Brand)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(25);

        builder.Property(x => x.Description)
           .IsRequired()
           .HasMaxLength(50);

        builder.Property(x => x.RealPrice)
               .IsRequired();

        builder.Property(x => x.SalePrice)
               .IsRequired();

        builder.Property(x => x.SalePercent)
               .IsRequired();

        builder.Property(x => x.StockQuantity)
               .IsRequired();

        builder.Property(x => x.Size)
               .IsRequired();
    }
}
