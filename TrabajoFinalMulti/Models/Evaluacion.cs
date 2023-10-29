using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoFinalMulti.Models
{
    public class Evaluacion
    {
        [Key]
        public int  Evaluacion_Id { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }

        [ForeignKey("Curso")]
        public int Curso_Id { get; set; }
        public Curso Curso { get; set; }

        public ICollection<EvaluacionPorEstudiante> EvaluacionPorEstudiantes { get; set; }
    }
}
