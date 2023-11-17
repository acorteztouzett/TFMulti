using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.ViewModel
{
    public class HorarioCursoViewModel
    {
        public List<Horario> ListaHorarios { get; set; }

        public Curso Curso { get; set; }
        public Aula Aula { get; set; }
        public Horario Horario { get; set; }

        public int HorarioSeleccionado { get; set; }

        public int EliminarHorario { get; set; }
    }
}
