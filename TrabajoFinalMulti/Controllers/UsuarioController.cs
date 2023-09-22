using Microsoft.AspNetCore.Mvc;
using TrabajoFinalMulti.Data;
using TrabajoFinalMulti.Models;
using TrabajoFinalMulti.ViewModel;

namespace TrabajoFinalMulti.Controllers
{
    public class UsuarioController : Controller
    {
        public readonly ApplicationDbContext objUsuario;

        public UsuarioController(ApplicationDbContext dbContext)
        {
            objUsuario = dbContext;
        }
        //REGISTRAR USUARIO:
        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarUsuario(RegistroViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.TipoUsuario == "docente")
                {
                    var docente = new Docente
                    {
                        Docente_Nombre = viewModel.Nombre,
                        Docente_Correo = viewModel.Correo,
                        Docente_Contraseña = viewModel.Contraseña,
                    };
                    objUsuario.Docente.Add(docente);
                }
                else if (viewModel.TipoUsuario == "estudiante")
                {
                    var estudiante = new Estudiante
                    {
                        Estudiante_Nombre = viewModel.Nombre,
                        Estudiante_Correo = viewModel.Correo,
                        Estudiante_Contraseña = viewModel.Contraseña,
                    };
                    objUsuario.Estudiante.Add(estudiante);
                }

                objUsuario.SaveChanges();
                return RedirectToAction("RegistrarUsuario");
            }
            return View(viewModel);
        }
    }
}
