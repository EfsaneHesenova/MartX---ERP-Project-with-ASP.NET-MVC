using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartX.DAL.Configurations
{
    public class CheckoutItemConfigurations : IEntityTypeConfiguration<CheckoutItem>
    {
        public void Configure(EntityTypeBuilder<CheckoutItem> builder)
        {
            builder.HasOne(x => x.Checkout)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CheckoutId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
               .IsRequired();

            builder.Property(x => x.TotalPrice)
               .IsRequired();
        }
    }
}
