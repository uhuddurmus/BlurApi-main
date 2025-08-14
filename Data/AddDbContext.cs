using BlurApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlurApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Console.WriteLine("Connected to database.");
        }

        public DbSet<Company> Companies { get; set; }
    }
}
