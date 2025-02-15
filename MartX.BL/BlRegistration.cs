using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using MartX.BL.ExternalServices.Abstractions;
using MartX.BL.ExternalServices.Implementations;
using MartX.BL.Profiles.AccountProfiles;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace MartX.BL;

public static class BlRegistration
{
    public static void AddBlServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationClientsideAdapters();
        services.AddFluentValidationAutoValidation();
        services.AddAutoMapper(typeof(AccountProfile));

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDocumentImageUrlService, DocumentImageUrlService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IFileUploadService, FileUploadService>();
    }
}
