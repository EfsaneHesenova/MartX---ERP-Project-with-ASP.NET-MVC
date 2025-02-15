using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartX.DAL.Configurations;

public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasOne(x => x.Department)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(25);

        builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(25);

        builder.Property(x => x.Age)
               .IsRequired();

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(25);

        builder.Property(x => x.PhoneNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.ImageUrl)
               .IsRequired()
               .HasMaxLength(60);

        builder.Property(x => x.Address)
               .IsRequired()
               .HasMaxLength(50);

    }
}
