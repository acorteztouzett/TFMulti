using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoFinalMulti.Models
{
    public class EvaluacionPorEstudiante
    {
        [Key]
        public int EvaluacionPorEstudiante_Id { get; set; }

        [ForeignKey("Evaluacion")]
        public int Evaluacion_Id { get; set; }
        public virtual Evaluacion Evaluacion { get; set; }

        [ForeignKey("Estudiante")]
        public int Estudiante_Id { get; set; }
        public virtual Estudiante Estudiante { get; set; }
        public float Nota { get; set; }
    }
}
