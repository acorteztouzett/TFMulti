using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class EvaluacionPorCurso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EvaluacionPorCurso_Id { get; set; }

        [ForeignKey("Curso")]
        public int Curso_Id { get; set; }
        public Curso Curso { get; set; }

        [ForeignKey("Evaluacion")]
        public int Evaluacion_Id { get; set; }
        public Evaluacion Evaluacion { get; set; }
    }
}

