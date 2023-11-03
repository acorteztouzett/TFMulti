using Microsoft.AspNetCore.Mvc.Rendering;
using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.ViewModel
{
    public class CursoEstudianteVM
    {
        public EstudiantesPorCurso EstudiantesPorCurso { get; set; }
        public Curso Curso { get; set; }
        public IEnumerable<EstudiantesPorCurso> ListaEstudiantesPorCurso { get; set; }
        public IEnumerable<SelectListItem> ListaEstudiantes { get; set; }

        public int Aforo { get; set; }

    }
}
