using MartX.DAL;
using MartX.BL;
using MartX.Core.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDalServices(builder.Configuration);
builder.Services.AddBlServices();
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/account/login";
    opt.AccessDeniedPath = "/";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Roles.Admin.ToString(), policy => policy.RequireRole("Admin"));
    options.AddPolicy(Roles.Boss.ToString(), policy => policy.RequireRole("Boss"));
    options.AddPolicy(Roles.Adminstrator.ToString(), policy => policy.RequireRole("Adminstrator"));
    options.AddPolicy(Roles.Cashier.ToString(), policy => policy.RequireRole("Cashier"));
    options.AddPolicy(Roles.Worker.ToString(), policy => policy.RequireRole("Worker"));
});



var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}"
          );

app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
          );
app.Run();
