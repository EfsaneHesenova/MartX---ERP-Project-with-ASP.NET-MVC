using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace MartX.DAL.Contexts;

public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<CheckoutItem> CheckoutItems { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DocumentImageUrl> DocumentImageUrls { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "6d16558a-ba79-4fb5-9717-bd333cfc2b0d", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "ccef448d-2a82-45c6-af5f-00edab215245", Name = "Boss", NormalizedName = "BOSS" },
            new IdentityRole { Id = "d1256ec7-5dd3-4e54-a75c-f9b0717544bd", Name = "Adminstrator", NormalizedName = "ADMINSTRATOR" },
            new IdentityRole { Id = "45fcd46e-b0cf-49ef-ba44-270899956722", Name = "Cashier", NormalizedName = "CASHIER" },
            new IdentityRole { Id = "287f30c4-6d0f-4687-b117-49d 810376603", Name = "Worker", NormalizedName = "WORKER" }

        );



        IdentityUser admin = new()
        {
            Id = "c10c9801-9957-4018-8e48-0c7812d47b50",
            UserName = "admin",
            NormalizedUserName = "ADMIN"
        };

        PasswordHasher<IdentityUser> hasher = new();
        admin.PasswordHash = hasher.HashPassword(admin, "admin123");

        builder.Entity<IdentityUser>().HasData(admin);

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = admin.Id, RoleId = "6d16558a-ba79-4fb5-9717-bd333cfc2b0d" }
        );

        base.OnModelCreating(builder);
    }
}
