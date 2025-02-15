using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartX.DAL.Configurations;

public class SupplierConfigurations : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {

        builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

        builder.Property(x => x.Address)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(25);

        builder.Property(x => x.ContactPerson)
               .IsRequired()
               .HasMaxLength(25);

    }
}
