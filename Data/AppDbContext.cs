using Microsoft.EntityFrameworkCore;
using prueba.API.Model;

namespace prueba.API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Meals> Meals { get; set; }
    }
}