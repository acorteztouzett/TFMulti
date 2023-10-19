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
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<AnuncioInformativo> AnuncioInformativo { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Apoderado> Apoderado { get; set; }
    }
}
