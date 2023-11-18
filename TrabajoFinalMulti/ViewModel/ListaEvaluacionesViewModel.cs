using TrabajoFinalMulti.Models;
namespace TrabajoFinalMulti.ViewModel
{
    public class ListaEvaluacionesViewModel
    {
        public Curso Curso { get; set; }
        public IEnumerable<Evaluacion> Evaluaciones { get; set; }
        public IEnumerable<EvaluacionPorEstudiante> Notas { get; set; }
    }

    public class RegistroEvaluacion
    {
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public int Curso_Id { get; set; }
    }
}
