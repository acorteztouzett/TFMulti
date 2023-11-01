using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class Periodo
    {
        [Key]
        public int Periodo_Id { get; set; }

        [Required(ErrorMessage = "El período es obligatorio")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Ingrese un año válido con 4 dígitos")]
        public string Periodo_Año { get; set; }

        [Required(ErrorMessage = "La fecha de inicio del período es obligatoria")]
        public DateTime Periodo_FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de finalización del período es obligatoria")]
        public DateTime Periodo_FechaFin { get; set; }

        public List<Aula> Aula { get; set; }
    }
}
