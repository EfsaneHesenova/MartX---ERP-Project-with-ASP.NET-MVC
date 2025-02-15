using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.DAL.Contexts;
using MartX.DAL.Repositories.Abstractions;
using MartX.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MartX.DAL;

public static class DalRegistration
{
    public static void AddDalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            opt.UseSqlServer(configuration.GetConnectionString("MsSql"));
        });

        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        services.AddScoped<IBrandReadRepository, BrandReadRepository>();
        services.AddScoped<IBrandWriteRepository, BrandWriteRepository>();
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
        services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
        services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
        services.AddScoped<IDocumentImageUrlReadRepository, DocumentImageUrlReadRepository>();
        services.AddScoped<IDocumentImageUrlWriteRepository, DocumentImageUrlWriteRepository>();
        services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
        services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<ISupplierReadRepository, SupplierReadRepository>();
        services.AddScoped<ISupplierWriteRepository, SupplierWriteRepository>();
    }
}
