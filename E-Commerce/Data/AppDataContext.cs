using E_Commerce.Configuration;
using E_Commerce.Models;
using E_Commerce.viewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
namespace E_Commerce.Data
{
    public class AppDataContext : IdentityDbContext<ApplicationUser>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options): base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EntityApplictaionUserConfiguration());
            builder.Entity<RegisterVm>().HasNoKey();
        }
        public DbSet<E_Commerce.viewModels.RegisterVm> RegisterVm { get; set; } = default!;
    }
}
