using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.ViewModel
{
    public class AsesoriaViewModel
    {
        public Curso Curso { get; set; }
        public IEnumerable<Asesoria> Asesorias { get; set; }
    }
        public class RegistroAsesoria
        {
            public string Tema { get; set; }
            public string Fecha { get; set; }
            public int Curso_Id { get; set; }
        }
   }

