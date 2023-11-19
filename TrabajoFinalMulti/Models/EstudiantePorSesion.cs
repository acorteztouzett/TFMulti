using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalMulti.Models
{
    public class EstudiantePorSesion
    {
        [Key]
        public int EstudiantesPorSesion_Id { get; set; }

        [ForeignKey("Sesion")]
        public int Sesion_Id { get; set; }
        public Sesion Sesion { get; set; }

        [ForeignKey("Estudiante")]
        public int Estudiante_Id { get; set; }
        public Estudiante Estudiante { get; set; }

        public bool Asistio { get; set; }

    }
}
