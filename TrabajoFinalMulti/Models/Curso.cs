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

        [ForeignKey("Docente")]
        public int Docente_Id { get; set; }
        public Docente Docente { get; set; }
    }
}
