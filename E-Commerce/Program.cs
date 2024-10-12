using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No Connection String was found");
            builder.Services.AddDbContext<AppDataContext>(option => option.UseSqlServer(connectionString));
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(1));
            //Register Database Connection
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
               option =>
              {
                  option.Password.RequireUppercase = false;
                  option.Password.RequireLowercase = false;
                  option.Password.RequireNonAlphanumeric = false;

              })
                .AddEntityFrameworkStores<AppDataContext>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
