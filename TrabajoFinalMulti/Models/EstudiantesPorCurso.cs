using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class EstudiantesPorCurso
    {

        [Key]
        public int EstudiantesPorCurso_Id { get; set; }

        [ForeignKey("Curso")]
        public int Curso_Id { get; set; }
        public Curso Curso { get; set; }

        [ForeignKey("Estudiante")]
        public int Estudiante_Id { get; set; }
        public Estudiante Estudiante { get; set; }
    }
}
