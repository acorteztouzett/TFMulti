using Microsoft.AspNetCore.Mvc;
using TrabajoFinalMulti.Data;
using TrabajoFinalMulti.Models;
using TrabajoFinalMulti.ViewModel;

namespace TrabajoFinalMulti.Controllers
{
    public class VistasUsuariosController : Controller
    {
        public readonly ApplicationDbContext objUsuario;

        public VistasUsuariosController(ApplicationDbContext dbContext)
        {
            objUsuario = dbContext;
        }
        public IActionResult VistaAdmin()
        {
            List<Estudiante> listaEstudiante = objUsuario.Estudiante.ToList();
            return View(listaEstudiante);
        }
    }
}
