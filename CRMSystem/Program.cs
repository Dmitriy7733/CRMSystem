using Microsoft.EntityFrameworkCore;
using CRMSystem.DB;
using CRMSystem.Models;
using CRMSystem.Repository;
using Microsoft.AspNetCore.Identity;
using CRMSystem.Services.Implementations;
using CRMSystem.Services.Interfaces;
using CRMSystem.Repository.Implementations;
using CRMSystem.Repository.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var crmConnectionString = builder.Configuration.GetSection("Data:CrmDb:ConnectionString").Value;
var identityConnectionString = builder.Configuration.GetSection("Data:CrmIdentity:ConnectionString").Value;


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(crmConnectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()));

builder.Services.AddDbContext<AppIdentityContext>(options =>
    options.UseSqlServer(identityConnectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityContext>() 
    .AddDefaultTokenProviders();

//builder.Services.AddTransient<IAdminService, AdminService>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();


// ��������� �������� ������
SeedData.EnsurePopulated(app);
await IdentitySeedData.EnsurePopulatedAsync(app);

// ��������� �������������� ��
app.UseDeveloperExceptionPage();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// ��������� ��������� ������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}"); // �������� ����� �� Admin

app.MapControllerRoute(
    name: "manager",
    pattern: "{controller=Manager}/{action=Index}");
app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=Index}");

// ��������� ��������� ������������
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "manager",
    pattern: "{controller=Manager}/{action=Index}");
app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=Index}");*/

// ������ ����������
await app.RunAsync();
//app.Run();