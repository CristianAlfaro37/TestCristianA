using Microsoft.EntityFrameworkCore;
using PruebaCristianAlfaro.Models;

namespace PruebaCristianAlfaro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
    }

}
