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

//builder.Services.AddTransient<IClientService, ClientService>();

//builder.Services.AddTransient<IOrderService, OrderService>();
//builder.Services.AddTransient<IOrderRepository, OrderRepository>();

//builder.Services.AddTransient<IAdminService, AdminService>();

//builder.Services.AddScoped(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();


// Начальная загрузка данных
SeedData.EnsurePopulated(app);
//await IdentitySeedData.EnsurePopulatedAsync(app);

// Настройка промежуточного ПО
app.UseDeveloperExceptionPage();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Настройка маршрутов контроллеров
app.MapControllerRoute(
    name: "manager",
    pattern: "{controller=Manager}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=Index}/{id?}");


// Запуск приложения
app.Run();