using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartX.DAL.Configurations;

public class CheckoutConfigurations : IEntityTypeConfiguration<Checkout>
{
    public void Configure(EntityTypeBuilder<Checkout> builder)
    {
        builder.Property(x => x.TotalPrice)
                .IsRequired();                   
    }
}
