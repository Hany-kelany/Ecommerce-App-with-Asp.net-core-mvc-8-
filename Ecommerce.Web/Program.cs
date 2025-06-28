
using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Entities.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options
                                 .UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));

builder.Services.AddIdentity<IdentityUser , IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();



builder.Services.AddRazorPages();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "Customer",
    pattern: "{area=Admin}/{controller=Category}/{action=Index}/{id?}")
    .WithStaticAssets();





app.Run();
