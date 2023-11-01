using Microsoft.EntityFrameworkCore;
using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //Escribir los modelos
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Docente> Docente { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }  
        public DbSet<AnuncioInformativo> AnuncioInformativo { get; set; }
        public DbSet<Periodo> Periodo { get; set; }
        public DbSet<Aula> Aula { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Apoderado> Apoderado { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<EstudiantesPorCurso> EstudiantesPorCursos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstudiantesPorCurso>().HasKey(exc => new
            {
                exc.Estudiante_Id,
                exc.Curso_Id
            });
        }

        public DbSet<EvaluacionPorEstudiante> EvaluacionPorEstudiantes { get; set; }
        
        public DbSet<EstudiantePorSesion> EstudiantesPorSesions { get; set; }
        public DbSet<Sesion> Sesiones { get; set;}

    }
}
