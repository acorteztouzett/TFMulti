using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class Horario
    {
        [Key]
        public int Horario_Id { get; set; }  
        public string Hora_Inicio { get; set; }
        public string Hora_Fin { get; set; }
        public string Dia { get; set; }
    }
}
