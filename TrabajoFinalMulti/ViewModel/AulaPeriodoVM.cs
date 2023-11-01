using Microsoft.AspNetCore.Mvc.Rendering;
using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.ViewModel
{
    public class AulaPeriodoVM
    {
        public Aula Aula { get; set; }
        public IEnumerable<SelectListItem> ListaPeriodos { get; set; }

    }
}
