using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartX.DAL.Configurations;

public class DocumentImageUrlConfigurations : IEntityTypeConfiguration<DocumentImageUrl>
{
    public void Configure(EntityTypeBuilder<DocumentImageUrl> builder)
    {
        builder.HasOne(X => X.Employee)
            .WithMany(x => x.DocumentImageUrls)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(25);

        builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(60);
    }
}
