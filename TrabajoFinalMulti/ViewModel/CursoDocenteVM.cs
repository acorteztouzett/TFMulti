using Microsoft.AspNetCore.Mvc.Rendering;
using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.ViewModel
{
    public class CursoDocenteVM
    {

        public Curso Curso { get; set; }
        public IEnumerable<SelectListItem> ListaDocentes { get; set; }
        public IEnumerable<SelectListItem> ListaAulas { get; set; }

    }
}
