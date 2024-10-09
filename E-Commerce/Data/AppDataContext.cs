using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
namespace E_Commerce.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options): base(options)
        {
                
        }
    }
}
