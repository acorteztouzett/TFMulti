using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TrabajoFinalMulti.Models
{
    public class Asesoria
    {
        [Key]
        public int Asesoria_Id { get; set; }

        [Required(ErrorMessage = "El Tema es obligatorio")]
        public string Tema { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public string Fecha { get; set; }

        [ForeignKey("Curso")]
        public int Curso_Id { get; set; }
        public Curso Curso { get; set; }

    }
}
