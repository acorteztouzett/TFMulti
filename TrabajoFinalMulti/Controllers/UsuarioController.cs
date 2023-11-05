using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinalMulti.Data;
using TrabajoFinalMulti.Models;
using TrabajoFinalMulti.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace TrabajoFinalMulti.Controllers
{
    public class UsuarioController : Controller
    {
        public readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public void CheckAndSetSession()
        {
            var session = HttpContext.Session.GetInt32("Key");
            if(session == null)
            {
                HttpContext.Session.SetInt32("Key", 1);
            }
            else
            {
                var newvalue = (int)session + 1;
                HttpContext.Session.SetInt32("Key", newvalue);
            }
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (HttpContext.Session.GetInt32("Key") == null || HttpContext.Session.GetInt32("Key") < 3)
            {
                // Verificar las credenciales ingresadas en la tabla de Administrador
                var admin = _context.Administrador.SingleOrDefault(a => a.Admin_Correo == viewModel.Correo && EF.Functions.Collate(a.Admin_Contraseña, "SQL_Latin1_General_CP1_CS_AS") == viewModel.Contraseña);

                // Verificar las credenciales ingresadas en la tabla de Docente
                var docente = _context.Docente.SingleOrDefault(d => d.Docente_Correo == viewModel.Correo && EF.Functions.Collate(d.Docente_Contraseña, "SQL_Latin1_General_CP1_CS_AS") == viewModel.Contraseña);

                // Verificar las credenciales ingresadas en la tabla de Estudiante
                var estudiante = _context.Estudiante.SingleOrDefault(e => e.Estudiante_Correo == viewModel.Correo && EF.Functions.Collate(e.Estudiante_Contraseña, "SQL_Latin1_General_CP1_CS_AS") == viewModel.Contraseña);

                // Comprobar si las credenciales coinciden en alguna de las tablas
                if (admin != null)
                {
                    // Credenciales válidas para Administrador, redirigir a la página correspondiente
                    return RedirectToAction("RegistrarUsuario", "Administrador");
                }
                else if (docente != null)
                {
                    // Credenciales válidas para Docente, redirigir a la página correspondiente

                    HttpContext.Session.SetString("SDocente", JsonConvert.SerializeObject(docente));
                    return RedirectToAction("Index", "Docente");
                }
                else if (estudiante != null)
                {
                    // Credenciales válidas para Estudiante, redirigir a la página correspondiente
                    HttpContext.Session.SetString("SUsuario", JsonConvert.SerializeObject(estudiante));
                    return RedirectToAction("Index", "Estudiante");
                }
                else
                {
                    // Autenticación fallida, mostrar la alerta
                    CheckAndSetSession();
                    ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                    ViewBag.ShowError = true;
                    TempData["Error"] = "Correo o contraseña incorrectos";
                    return View(viewModel);
                }
            }
            else
            {
                // Autenticación fallida, mostrar la alerta
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                ViewBag.ShowError = true;
                TempData["Error"] = "Inténtalo más tarde";
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Perfil(int model = 0)
        {
            var estudiante = _context.Estudiante.SingleOrDefault(e => e.Estudiante_Id == model);
            return View(estudiante);
        }

        //CERRAR SESIÓN
        [HttpPost]
        public IActionResult Logout()
        {
            // Redirige al usuario a la página de inicio o a donde desees después de cerrar sesión.
            return RedirectToAction("Login");
        }      

        public IActionResult ActualizarUsuario(ActualizarViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var objeto = JsonConvert.DeserializeObject<Estudiante>(HttpContext.Session.GetString("SUsuario"));
                var estudiante = _context.Estudiante.SingleOrDefault(a => a.Estudiante_Id == objeto.Estudiante_Id);
                estudiante.Estudiante_Nombre = viewmodel.Nombre;
                estudiante.Estudiante_Correo = viewmodel.Correo;
                estudiante.Estudiante_Contraseña = viewmodel.Contraseña;

                _context.Estudiante.Update(estudiante);
                _context.SaveChanges();

                HttpContext.Session.Clear();
                HttpContext.Session.SetString("SUsuario", JsonConvert.SerializeObject(estudiante));
            }
            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult ActualizarDocente(ActualizarViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var objeto = JsonConvert.DeserializeObject<Docente>(HttpContext.Session.GetString("SDocente"));
                var docente = _context.Docente.SingleOrDefault(a => a.Docente_Id == objeto.Docente_Id);
                docente.Docente_Nombre = viewmodel.Nombre;
                docente.Docente_Correo = viewmodel.Correo;
                docente.Docente_Contraseña = viewmodel.Contraseña;

                _context.Docente.Update(docente);
                _context.SaveChanges();

                HttpContext.Session.Clear();
                HttpContext.Session.SetString("SDocente", JsonConvert.SerializeObject(docente));
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
