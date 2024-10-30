
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticPizza.DAL;
using PracticPizza.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddIdentity<AppUser,IdentityRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;


    option.User.RequireUniqueEmail = true;
    option.Lockout.MaxFailedAccessAttempts = 3;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

 var app = builder.Build();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();


app.MapControllerRoute(
    "Default",
    "{area:exists}/{controller=home}/{action=index}/{id?}"
    );

app.MapControllerRoute(
    "Default",
    "{controller=home}/{action=index}/{id?}"
    );

          

app.Run();
 