using Microsoft.EntityFrameworkCore;
using CRMSystem.DB;
using CRMSystem.Models;
using CRMSystem.Repository;
using Microsoft.AspNetCore.Identity;
using CRMSystem.Services.Implementations;
using CRMSystem.Services.Interfaces;
using CRMSystem.Repository.Implementations;
using CRMSystem.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var crmConnectionString = builder.Configuration.GetSection("Data:CrmDb:ConnectionString").Value;
var identityConnectionString = builder.Configuration.GetSection("Data:CrmIdentity:ConnectionString").Value;

builder.Services.AddDbContext<AppIdentityContext>(options => options.UseSqlServer(identityConnectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppIdentityContext>().AddDefaultTokenProviders();


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(crmConnectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()));


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();


builder.Services.AddTransient<IManagerRepository, ManagerRepository>();
builder.Services.AddTransient<IManagerService, ManagerService>();



var app = builder.Build();



SeedData.EnsurePopulated(app);
await IdentitySeedData.EnsurePopulatedAsync(app);


app.UseDeveloperExceptionPage();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

await app.RunAsync();
