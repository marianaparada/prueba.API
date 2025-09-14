using Microsoft.EntityFrameworkCore;
namespace prueba.API.Data;
using prueba.API.Model;
public class AppDbContext
{
    public DbSet<Meals> Meals { get; set; }
}