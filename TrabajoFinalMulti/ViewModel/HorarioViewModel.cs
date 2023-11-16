using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.ViewModel
{
    public class HorarioViewModel
    {
        public Curso Curso { get; set; }
        public IEnumerable<Horario> Horarios { get; set; }
    }
}
