using Microsoft.EntityFrameworkCore;
using CRMSystem.DB;
using CRMSystem.Models;
using CRMSystem.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var crmConnectionString = builder.Configuration.GetSection("Data:CrmDb:ConnectionString").Value;
var identityConnectionString = builder.Configuration.GetSection("Data:CrmIdentity:ConnectionString").Value;


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(crmConnectionString));

builder.Services.AddDbContext<AppIdentityContext>(options => options.UseSqlServer(identityConnectionString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityContext>() 
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();


SeedData.EnsurePopulated(app);
await IdentitySeedData.EnsurePopulatedAsync(app);
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();