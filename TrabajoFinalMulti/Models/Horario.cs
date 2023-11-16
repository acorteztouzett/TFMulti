using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoFinalMulti.Models
{
    public class Horario
    {
        [Key]
        public int Horario_Id { get; set; }

        
        public string Hora_Inicio { get; set; }
        public string Hora_Fin { get; set; }
        public string Dia { get; set; }
        [ForeignKey("Curso")]
        public int Curso_Id { get; set; }
        public Curso Curso { get; set; }
    }
}
