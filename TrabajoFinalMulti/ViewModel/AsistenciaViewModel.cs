using TrabajoFinalMulti.Models;
namespace TrabajoFinalMulti.ViewModel

{
    public class AsistenciaViewModel
    {
        public int Curso_Id { get; set; }
        public Sesion Sesion { get; set; }
        public IEnumerable<EstudiantePorSesion> Estudiantes { get; set; }

    }
}
