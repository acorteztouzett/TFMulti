using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TrabajoFinalMulti.Models
{
    public class Asesoria
    {
        [Key]
        public int Asesoria_Id { get; set; }
        public string Tema { get; set; }
        public string Fecha { get; set; }

        [ForeignKey("Curso")]
        public int Curso_Id { get; set; }
        public Curso Curso { get; set; }

    }
}
