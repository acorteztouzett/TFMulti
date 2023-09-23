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

        //LISTAR USUARIOS:
        public IActionResult ListaUsuarios()
        {
            var docentes = objUsuario.Docente.ToList();
            var estudiantes = objUsuario.Estudiante.ToList();

            var viewModel = new UsuariosViewModel
            {
                ListaDocente = docentes,
                ListaEstudiante = estudiantes
            };

            return View(viewModel);
        }

        //ELIMINAR DOCENTE O ESTUDIANTE:
        [HttpGet]
        public IActionResult EliminarUsuario(int? id, string tipo)
        {
            if (id == null || string.IsNullOrEmpty(tipo))
            {
                // Maneja la falta de ID o tipo de usuario como un error o redirecciona a una página de error
                return RedirectToAction("Error");
            }

            if (tipo == "Docente")
            {
                var docente = objUsuario.Docente.FirstOrDefault(d => d.Docente_Id == id);
                if (docente != null)
                {
                    objUsuario.Docente.Remove(docente);
                    objUsuario.SaveChanges();
                }
            }
            else if (tipo == "Estudiante")
            {
                var estudiante = objUsuario.Estudiante.FirstOrDefault(e => e.Estudiante_Id == id);
                if (estudiante != null)
                {
                    objUsuario.Estudiante.Remove(estudiante);
                    objUsuario.SaveChanges();
                }
            }
            else
            {
                // Maneja el tipo de usuario desconocido como un error o redirecciona a una página de error
                return RedirectToAction("Error");
            }

            return RedirectToAction("ListaUsuarios", "VistasUsuarios");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarUsuario(string tipo, int id)
        {
            if (string.IsNullOrEmpty(tipo) || id <= 0)
            {
                // Manejar el error de tipo inválido o ID no válido
                return RedirectToAction("Error");
            }

            if (tipo == "Docente")
            {
                // Redirigir a la vista de edición de Docente
                return RedirectToAction("EditarDocente", new { id });
            }
            else if (tipo == "Estudiante")
            {
                // Redirigir a la vista de edición de Estudiante
                return RedirectToAction("EditarEstudiante", new { id });
            }
            else
            {
                // Manejar el tipo de usuario desconocido como un error o redirecciona a una página de error
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult EditarDocente(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var docente = objUsuario.Docente.FirstOrDefault(d => d.Docente_Id == id);

            if (docente == null)
            {
                // Manejar el caso en el que no se encuentre el docente
                return RedirectToAction("Error","Shared");
            }

            // Obtener la contraseña actual del docente
            string contraseñaActual = docente.Docente_Contraseña;

            // Establecer la contraseña actual en el modelo
            docente.Docente_Contraseña = contraseñaActual;

            return View(docente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarDocente(Docente docente)
        {
            // Agregar líneas de registro o depuración para verificar el estado del modelo
            System.Diagnostics.Debug.WriteLine("Contraseña actual: " + docente.Docente_Contraseña);
            System.Diagnostics.Debug.WriteLine("Nombre actual: " + docente.Docente_Nombre);
            System.Diagnostics.Debug.WriteLine("Correo actual: " + docente.Docente_Correo);

            if (ModelState.IsValid)
            {
                // Realizar la actualización solo si los campos tienen valores válidos
                if (!string.IsNullOrEmpty(docente.Docente_Contraseña) && !string.IsNullOrEmpty(docente.Docente_Nombre) && !string.IsNullOrEmpty(docente.Docente_Correo))
                {
                    objUsuario.Docente.Update(docente);
                    objUsuario.SaveChanges();
                    return RedirectToAction(nameof(ListaUsuarios));
                }
                else
                {
                    // Manejar el caso en el que al menos uno de los campos está vacío
                    ModelState.AddModelError("", "Todos los campos son obligatorios.");
                }
            }
            return View(docente);
        }



        [HttpGet]
        public IActionResult EditarEstudiante(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var estudiante = objUsuario.Estudiante.FirstOrDefault
                (d => d.Estudiante_Id == id);
            return View(estudiante);
        }
        
    }
}
