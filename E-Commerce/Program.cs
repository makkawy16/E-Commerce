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
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(1));
            //Register Database Connection
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();

            

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

            //app.MapControllerRoute(
              //  name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");

            
                app.MapControllerRoute(
                  name: "default",
                  pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}" );
            app.MapControllerRoute(
                  name: "Customer",
                  pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
