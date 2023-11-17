using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoFinalMulti.Models
{
    public class Curso
    {
        [Key]
        public int Curso_Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Curso_Nombre { get; set; }
        public int Cantidad_Horas { get; set; }

        [ForeignKey("Docente")]
        public int Docente_Id { get; set; }
        public Docente Docente { get; set; }

        [ForeignKey("Aula")]
        public int Aula_Id { get; set; }
        public Aula Aula { get; set; }

        
        public ICollection<Evaluacion> Evaluacions { get; set; }
        public ICollection<EstudiantesPorCurso> EstudiantesPorCursos { get; set; }
        public ICollection<Sesion> Sesiones { get; set; }

        public ICollection<Horario> Horarios { get; set; }
    }
}
