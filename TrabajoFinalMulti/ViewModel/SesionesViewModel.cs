using TrabajoFinalMulti.Models;
namespace TrabajoFinalMulti.ViewModel
{
    public class SesionesViewModel
    {
        public Curso Curso { get; set; }
        public IEnumerable<Sesion> Sesiones { get; set; }
    }
}
