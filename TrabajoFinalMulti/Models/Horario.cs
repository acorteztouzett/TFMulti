using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoFinalMulti.Models
{
    public class Horario
    {
        [Key]
        public int Horario_Id { get; set; }

        [Required(ErrorMessage = "El día es obligatorio.")]
        public string Dia { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public string Hora_Inicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        public string Hora_Fin { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; }



        [ForeignKey("Aula")]
        public int Aula_Id { get; set; }
        public Aula Aula { get; set; }

        [ForeignKey("Curso")]
        public int? Curso_Id { get; set; }
        public Curso Curso { get; set; }

    }
}
