using TrabajoFinalMulti.Models;
namespace TrabajoFinalMulti.ViewModel

{
    public class NotasViewModel
    {
        public int Curso_Id { get; set; }
        public int Estudiante_Id { get; set; }
        public string Estudiante_Nombre { get; set; }
        public IEnumerable<EvaluacionPorEstudiante> NotasEstudiante { get; set; }

    }
}
