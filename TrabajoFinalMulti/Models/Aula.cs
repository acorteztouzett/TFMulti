using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoFinalMulti.Models
{
    public class Aula
    {
        [Key]
        public int Aula_Id { get; set; }

        [Required(ErrorMessage = "El nombre del aula es obligatorio")]
        public string Aula_Nombre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El aforo debe ser mayor que cero")]
        public int Aula_Aforo { get; set; }

        [Required(ErrorMessage = "El nivel educativo del aula es obligatorio")]
        public string Aula_NivelEducativo { get; set; }

        [Required(ErrorMessage = "El grado del aula es obligatorio")]
        public string Aula_Grado { get; set; }     

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Aula_Estado { get; set; }


        [ForeignKey("Periodo")]
        public int Periodo_Id { get; set; }
        public Periodo Periodo { get; set; }

        public List<Curso> Curso { get; set; }

        public ICollection<Horario> Horarios { get; set; }

    }
}
