using Microsoft.AspNetCore.Mvc;
using TrabajoFinalMulti.Data;
using TrabajoFinalMulti.Models;
using TrabajoFinalMulti.ViewModel;

namespace TrabajoFinalMulti.Controllers
{
    public class AdministradorController : Controller
    {
        public readonly ApplicationDbContext objUsuario;

        public AdministradorController(ApplicationDbContext dbContext)
        {
            objUsuario = dbContext;
        }

        /*-------------------REGISTRAR USUARIOS (DOCENTE O ESTUDIANTE)-------------------------------*/
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
                // Verificar si el correo ya está en uso
                if (objUsuario.Docente.Any(d => d.Docente_Correo == viewModel.Correo) ||
                    objUsuario.Estudiante.Any(e => e.Estudiante_Correo == viewModel.Correo))
                {
                    ModelState.AddModelError("Correo", "El correo ya está en uso.");
                }
                else if (!CumpleRequisitosContraseña(viewModel.Contraseña))
                {

                }
                else
                {
                    //Guardar
                    if (viewModel.TipoUsuario == "docente")
                    {
                        var docente = new Docente
                        {
                            Docente_Nombre = viewModel.Nombre,
                            Docente_Correo = viewModel.Correo,
                            Docente_Contraseña = viewModel.Contraseña,
                        };
                        objUsuario.Docente.Add(docente);
                        objUsuario.SaveChanges();

                        return RedirectToAction("ListaDocentes", "Administrador");
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
                        objUsuario.SaveChanges();

                        return RedirectToAction("ListaEstudiantes", "Administrador");
                    }
                }
            }

            return View(viewModel);
        }

        // Función para verificar si la contraseña cumple con los requisitos
        private bool CumpleRequisitosContraseña(string contraseña)
        {
            const int longitudMinima = 5;
            if (contraseña.Length < longitudMinima)
            {
                return false;
            }

            // Verificar si contiene al menos una letra mayúscula y un número
            return contraseña.Any(char.IsUpper) && contraseña.Any(char.IsDigit);
        }

        /*------------------------DOCENTES--------------------------------------*/
        //LISTAR DOCENTES:
        public IActionResult ListaDocentes()
        {
            List<Docente> listaDocentes = objUsuario.Docente.ToList();
            return View(listaDocentes);
        }

        [HttpGet]
        public IActionResult EditarDocente(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var docente = objUsuario.Docente.FirstOrDefault(c => c.Docente_Id == id);
            return View(docente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarDocente(Docente docente)
        {
            if (ModelState.IsValid)
            {
                if (objUsuario.Docente.Any(d => d.Docente_Correo == docente.Docente_Correo && d.Docente_Id != docente.Docente_Id))
                {
                    ModelState.AddModelError("Docente_Correo", "El correo ya está en uso.");
                    return View(docente); // Devuelve la vista con el modelo para que el usuario pueda corregir
                }
                objUsuario.Docente.Update(docente);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaDocentes));
            }
            return View(docente);
        }

        [HttpGet]
        public IActionResult EliminarDocente(int? id)
        {
            var docente = objUsuario.Docente.FirstOrDefault(c => c.Docente_Id == id);
            objUsuario.Docente.Remove(docente);
            objUsuario.SaveChanges();
            return RedirectToAction("ListaDocentes", "Administrador");
        }

        /*------------------------ESTUDIANTES--------------------------------------*/
        //LISTAR ESTUDIANTES:
        public IActionResult ListaEstudiantes()
        {
            List<Estudiante> listaEstudiantes = objUsuario.Estudiante.ToList();
            return View(listaEstudiantes);
        }

        [HttpGet]
        public IActionResult EditarEstudiante(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var estudiante = objUsuario.Estudiante.FirstOrDefault(c => c.Estudiante_Id == id);
            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarEstudiante(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                if (objUsuario.Estudiante.Any(d => d.Estudiante_Correo == estudiante.Estudiante_Correo && d.Estudiante_Id != estudiante.Estudiante_Id))
                {
                    ModelState.AddModelError("Estudiante_Correo", "El correo ya está en uso.");
                    return View(estudiante); // Devuelve la vista con el modelo para que el usuario pueda corregir
                }
                objUsuario.Estudiante.Update(estudiante);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaEstudiantes));
            }
            return View(estudiante);
        }

        [HttpGet]
        public IActionResult EliminarEstudiante(int? id)
        {
            var estudiante = objUsuario.Estudiante.FirstOrDefault(c => c.Estudiante_Id == id);
            objUsuario.Estudiante.Remove(estudiante);
            objUsuario.SaveChanges();
            return RedirectToAction("ListaEstudiantes", "Administrador");
        }


    }
}
