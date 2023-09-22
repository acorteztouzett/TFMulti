using Microsoft.EntityFrameworkCore;
using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //Escribir los modelos
        public DbSet<Docente> Docente { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
    }
}
