using Microsoft.EntityFrameworkCore;
using prueba.API.Model;

namespace prueba.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Meals> Meals { get; set; }
    }
}