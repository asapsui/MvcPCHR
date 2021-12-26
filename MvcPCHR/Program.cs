using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcPCHR.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

/*
 * this GetConnectionString method will search for the string provided inside the name 
 * ConnectionString in appsettings.json
 * 
 * So now our DbContext will be configured with the connectionstring
 */

builder.Services.AddDbContext<PCHRDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// this is to bind our identity with our dbcontext
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<PCHRDBContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// this is to enable authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
