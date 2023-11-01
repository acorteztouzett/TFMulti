using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

                    string carpetaUsuarios = "FotosUsuarios";
                    string subcarpeta = viewModel.TipoUsuario == "docente" ? "FotosDocentes" : "FotosEstudiantes";
                    
                    int nuevoId = 1;

                    if (viewModel.TipoUsuario == "docente" && objUsuario.Docente.Any())
                    {
                        nuevoId = objUsuario.Docente.Max(d => d.Docente_Id) + 1;
                    }
                    else if (viewModel.TipoUsuario == "estudiante" && objUsuario.Estudiante.Any())
                    {
                        nuevoId = objUsuario.Estudiante.Max(e => e.Estudiante_Id) + 1;
                    }
                    string nombreArchivo = $"{nuevoId}.jpg";
                    string rutaRelativa = Path.Combine(carpetaUsuarios, subcarpeta, nombreArchivo);
                    string rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaRelativa);

                    // Guardar la foto si se proporciona
                    if (viewModel.Foto != null)
                    {
                        using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
                        {
                            viewModel.Foto.CopyTo(fileStream);
                        }
                    } else
                    {
                        rutaRelativa = null;
                    }

                    //Guardar
                    if (viewModel.TipoUsuario == "docente")
                    {
                        var docente = new Docente
                        {
                            Docente_Nombre = viewModel.Nombre,
                            Docente_Apellido = viewModel.Apellido,
                            Docente_FechaNacimiento = viewModel.FechaNacimiento,
                            Docente_DNI = viewModel.DNI,
                            Docente_Celular = viewModel.Celular,
                            Docente_Correo = viewModel.Correo,
                            Docente_Contraseña = viewModel.Contraseña,
                            Docente_Foto = rutaRelativa,
                            Docente_Estado = "Activo",
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
                            Estudiante_Apellido = viewModel.Apellido,
                            Estudiante_FechaNacimiento = viewModel.FechaNacimiento,
                            Estudiante_DNI = viewModel.DNI,
                            Estudiante_Celular = viewModel.Celular,
                            Estudiante_Correo = viewModel.Correo,
                            Estudiante_Contraseña = viewModel.Contraseña,
                            Estudiante_Foto = rutaRelativa,
                            Estudiante_Estado = "Activo",
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
        public IActionResult EditarDocente(Docente docente, IFormFile nuevaFoto)
        {
            if (ModelState.IsValid)
            {
                if (objUsuario.Docente.Any(d => d.Docente_Correo == docente.Docente_Correo && d.Docente_Id != docente.Docente_Id))
                {
                    ModelState.AddModelError("Docente_Correo", "El correo ya está en uso.");
                    return View(docente); // Devuelve la vista con el modelo para que el usuario pueda corregir
                }
                else
                {

                    // Guardar la nueva foto si se proporciona
                    if (nuevaFoto != null)
                    {
                        string carpetaUsuarios = "FotosUsuarios";
                        string subcarpeta = "FotosDocentes";
                        string nombreArchivo = $"{docente.Docente_Id}.jpg";

                        string rutaRelativa = Path.Combine(carpetaUsuarios, subcarpeta, nombreArchivo);
                        string rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaRelativa);

                        using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
                        {
                            nuevaFoto.CopyTo(fileStream);
                        }

                        // Actualizar la propiedad de la foto en el objeto Docente
                        docente.Docente_Foto = rutaRelativa;
                    }
                    else if (string.IsNullOrEmpty(docente.Docente_Foto))
                    {
                        string carpetaUsuarios = "FotosUsuarios";
                        string subcarpeta = "FotosDocentes";
                        string nombreArchivo = $"{docente.Docente_Id}.jpg";

                        string rutaRelativa = Path.Combine(carpetaUsuarios, subcarpeta, nombreArchivo);

                        docente.Docente_Foto = rutaRelativa;
                    }

                    // Actualizar otros campos y guardar en la base de datos
                    objUsuario.Docente.Update(docente);
                    objUsuario.SaveChanges();

                    return RedirectToAction(nameof(ListaDocentes));
                }
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
            //List<Estudiante> listaEstudiantes = objUsuario.Estudiante.ToList();
            List<Estudiante> listaEstudiantes = objUsuario.Estudiante
            .Include(e => e.Apoderado) //incluir Apoderado
            .ToList();
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
        public IActionResult EditarEstudiante(Estudiante estudiante, IFormFile nuevaFoto)
        {
            if (ModelState.IsValid)
            {
                if (objUsuario.Estudiante.Any(d => d.Estudiante_Correo == estudiante.Estudiante_Correo && d.Estudiante_Id != estudiante.Estudiante_Id))
                {
                    ModelState.AddModelError("Docente_Correo", "El correo ya está en uso.");
                    return View(estudiante); // Devuelve la vista con el modelo para que el usuario pueda corregir
                }
                else
                {
                    // Guardar la nueva foto si se proporciona
                    if (nuevaFoto != null)
                    {
                        string carpetaUsuarios = "FotosUsuarios";
                        string subcarpeta = "FotosEstudiantes";
                        string nombreArchivo = $"{estudiante.Estudiante_Id}.jpg";

                        string rutaRelativa = Path.Combine(carpetaUsuarios, subcarpeta, nombreArchivo);
                        string rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rutaRelativa);

                        using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
                        {
                            nuevaFoto.CopyTo(fileStream);
                        }

                        // Actualizar la propiedad de la foto en el objeto Docente
                        estudiante.Estudiante_Foto = rutaRelativa;
                    }
                    else if (string.IsNullOrEmpty(estudiante.Estudiante_Foto))
                    {
                        string carpetaUsuarios = "FotosUsuarios";
                        string subcarpeta = "FotosEstudiantes";
                        string nombreArchivo = $"{estudiante.Estudiante_Id}.jpg";

                        string rutaRelativa = Path.Combine(carpetaUsuarios, subcarpeta, nombreArchivo);

                        estudiante.Estudiante_Foto = rutaRelativa;
                    }

                    // Actualizar otros campos y guardar en la base de datos
                    objUsuario.Estudiante.Update(estudiante);
                    objUsuario.SaveChanges();

                    return RedirectToAction(nameof(ListaEstudiantes));
                }
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

        /*------------------------APODERADOS PARA CADA ESTUDIANTE--------------------------------------*/
        [HttpGet]
        public IActionResult RegistrarApoderado(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var estudiante = objUsuario.Estudiante.Include(d => d.Apoderado).FirstOrDefault(u => u.Estudiante_Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarApoderado(Estudiante estudiante)
        {
            if (estudiante.Apoderado.Apoderado_Id == 0)
            {
                // Agrega el apoderado a la base de datos
                objUsuario.Apoderado.Add(estudiante.Apoderado);
                objUsuario.SaveChanges();

                // Actualiza la referencia del apoderado en el estudiante
                var estudianteBd = objUsuario.Estudiante.FirstOrDefault(u => u.Estudiante_Id == estudiante.Estudiante_Id);
                estudianteBd.Apoderado_Id = estudiante.Apoderado.Apoderado_Id;
                objUsuario.SaveChanges();             
            }
            return RedirectToAction(nameof(ListaEstudiantes));
        }  


        [HttpGet]
        public IActionResult EditarApoderado(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var apoderado = objUsuario.Apoderado.FirstOrDefault(c => c.Apoderado_Id == id);
            return View(apoderado);
        }

        [HttpPost]
        public IActionResult EditarApoderado(Apoderado apoderado)
        {
            if (ModelState.IsValid)
            {
                objUsuario.Apoderado.Update(apoderado);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaEstudiantes));
            }

            return View(apoderado);
        }

        [HttpGet]
        public IActionResult EliminarApoderado(int? id)
        {
            var apoderado = objUsuario.Apoderado.FirstOrDefault(c => c.Apoderado_Id == id);

            if (apoderado != null)
            {
                // Obten los estudiantes relacionados y establece Apoderado_Id a null
                var estudiantesRelacionados = objUsuario.Estudiante.Where(e => e.Apoderado_Id == id);
                foreach (var estudiante in estudiantesRelacionados)
                {
                    estudiante.Apoderado_Id = null;
                }

                objUsuario.Apoderado.Remove(apoderado);
                objUsuario.SaveChanges();
            }

            return RedirectToAction("ListaEstudiantes", "Administrador");
        }


        /*------------------------BUSQUEDA EN DOCENTES Y ESTUDIANTES--------------------------------------*/
        [HttpGet]
        public IActionResult BuscarDocente(string term)
        {
            // Realiza la búsqueda en tu base de datos y obtiene los resultados
            var resultados = objUsuario.Docente
                .Where(u => u.Docente_Nombre.Contains(term))
                .ToList();

            return PartialView("_TablaDocentes", resultados);
        }

        [HttpGet]
        public IActionResult BuscarEstudiante(string term)
        {
            // Realiza la búsqueda en tu base de datos y obtiene los resultados
            var resultados = objUsuario.Estudiante
                .Where(u => u.Estudiante_Nombre.Contains(term))
                .ToList();

            return PartialView("_TablaEstudiantes", resultados);
        }


        /*------------------------CREAR CURSOS Y ASIGNARLOS A UN DOCENTE--------------------------------------*/
        /*public IActionResult ListaCursos()
        {
            List<Curso> listaCursos = objUsuario.Curso.ToList();
            return View(listaCursos);
        }*/

        public IActionResult ListaCursos()
        {
            var cursos = objUsuario.Curso
                .Include(c => c.Docente) // Esto carga el docente relacionado
                .Include(c => c.Aula)
                .ToList();

            return View(cursos);
        }

        [HttpGet]
        public IActionResult RegistrarCurso()
        {
            CursoDocenteVM cursoDocenteAula = new CursoDocenteVM();
            cursoDocenteAula.ListaDocentes = objUsuario.Docente.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Docente_Nombre,
                Value = i.Docente_Id.ToString()
            });

            cursoDocenteAula.ListaAulas = objUsuario.Aula.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = $"{i.Aula_NivelEducativo} - {i.Aula_Grado}",
                Value = i.Aula_Id.ToString()
            });

            return View(cursoDocenteAula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarCurso(Curso curso)
        {
            if (ModelState.IsValid)
            {
                objUsuario.Curso.Add(curso);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaCursos));
            }

            CursoDocenteVM cursoDocenteAula = new CursoDocenteVM();
            cursoDocenteAula.ListaDocentes = objUsuario.Docente.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Docente_Nombre,
                Value = i.Docente_Id.ToString()
            });

            cursoDocenteAula.ListaAulas = objUsuario.Aula.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = $"{i.Aula_NivelEducativo} - {i.Aula_Grado}",
                Value = i.Aula_Id.ToString()
            });

            return View(cursoDocenteAula);
        }
        [HttpGet]
        public IActionResult EditarCurso(int? id)
        {
            if (id == null)
            {
                return View();
            }
            CursoDocenteVM cursoDocente = new CursoDocenteVM();
            cursoDocente.ListaDocentes = objUsuario.Docente.Select(i => new SelectListItem
            {
                Text = i.Docente_Nombre,
                Value = i.Docente_Id.ToString()
            });

            cursoDocente.Curso = objUsuario.Curso.FirstOrDefault(c => c.Curso_Id == id);
            if (cursoDocente == null)
            {
                return NotFound();
            }
            return View(cursoDocente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarCurso(CursoDocenteVM cursoDocenteVM)
        {
            if (cursoDocenteVM.Curso.Curso_Id == 0)
            {
                return View(cursoDocenteVM.Curso);
            }
            else
            {
                objUsuario.Curso.Update(cursoDocenteVM.Curso);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaCursos));
            }
        }

        [HttpGet]
        public IActionResult BorrarCurso(int? id)
        {
            var curso = objUsuario.Curso.FirstOrDefault(c => c.Curso_Id == id);
            objUsuario.Curso.Remove(curso);
            objUsuario.SaveChanges();
            return RedirectToAction(nameof(ListaCursos));
        }

        /*------------------------ANUNCIOS INFORMATIVOS--------------------------------------*/
        public IActionResult ListaAnuncioInformativo()
        {
            List<AnuncioInformativo> listaAnuncioInformativo = objUsuario.AnuncioInformativo.ToList();
            return View(listaAnuncioInformativo);
        }
        [HttpGet]
        public IActionResult RegistrarAnuncioInformativo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarAnuncioInformativo(AnuncioInformativo anuncio)
        {
            if (ModelState.IsValid)
            {
                objUsuario.AnuncioInformativo.Add(anuncio);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaAnuncioInformativo));
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditarAnuncioInformativo(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var anuncio = objUsuario.AnuncioInformativo.FirstOrDefault(c => c.Anuncio_Id == id);
            return View(anuncio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarAnuncioInformativo(AnuncioInformativo anuncio)
        {
            if (ModelState.IsValid)
            {
                objUsuario.AnuncioInformativo.Update(anuncio);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaAnuncioInformativo));
            }
            return View(anuncio);
        }

        [HttpGet]
        public IActionResult BorrarAnuncioInformativo(int? id)
        {
            var anuncio = objUsuario.AnuncioInformativo.FirstOrDefault(c => c.Anuncio_Id == id);
            objUsuario.AnuncioInformativo.Remove(anuncio);
            objUsuario.SaveChanges();
            return RedirectToAction(nameof(ListaAnuncioInformativo));
        }

        /*------------------------PERIODO--------------------------------------*/
        public IActionResult ListaPeriodos()
        {
            List<Periodo> listaPeriodos = objUsuario.Periodo.OrderBy(p => p.Periodo_Año).ToList();
            return View(listaPeriodos);
        }

        [HttpGet]
        public IActionResult RegistrarPeriodo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarPeriodo(Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                objUsuario.Periodo.Add(periodo);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaPeriodos));
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditarPeriodo(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var periodo = objUsuario.Periodo.FirstOrDefault(c => c.Periodo_Id == id);
            return View(periodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPeriodo(Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                objUsuario.Periodo.Update(periodo);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaPeriodos));
            }
            return View(periodo);
        }

        [HttpGet]
        public IActionResult BorrarPeriodo(int? id)
        {
            var periodo = objUsuario.Periodo.FirstOrDefault(c => c.Periodo_Id == id);
            objUsuario.Periodo.Remove(periodo);
            objUsuario.SaveChanges();
            return RedirectToAction(nameof(ListaPeriodos));
        }


        /*------------------------CREAR AULA Y ASIGNARLO A UN PERIODO--------------------------------------*/
        public IActionResult ListaAulas()
        {
            var aulas = objUsuario.Aula.Include(p => p.Periodo).ToList();

            return View(aulas);
        }

        [HttpGet]
        public IActionResult RegistrarAula()
        {
            AulaPeriodoVM aulaPeriodo = new AulaPeriodoVM();
            aulaPeriodo.ListaPeriodos = objUsuario.Periodo.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Periodo_Año,
                Value = i.Periodo_Id.ToString()
            });
            
            return View(aulaPeriodo);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarAula(Aula aula)
        {
            if (ModelState.IsValid)
            {
                objUsuario.Aula.Add(aula);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaAulas));
            }
            AulaPeriodoVM aulaPeriodo = new AulaPeriodoVM();
            aulaPeriodo.ListaPeriodos = objUsuario.Periodo.Select(i => new SelectListItem
            {
                Text = i.Periodo_Año,
                Value = i.Periodo_Id.ToString()
            });
            return View(aulaPeriodo);
        }

        [HttpGet]
        public IActionResult EditarAula(int? id)
        {
            if (id == null)
            {
                return View();
            }
            AulaPeriodoVM aulaPeriodo = new AulaPeriodoVM();
            aulaPeriodo.ListaPeriodos = objUsuario.Periodo.Select(i => new SelectListItem
            {
                Text = i.Periodo_Año,
                Value = i.Periodo_Id.ToString()
            });

            aulaPeriodo.Aula = objUsuario.Aula.FirstOrDefault(c => c.Aula_Id == id);
            if (aulaPeriodo == null)
            {
                return NotFound();
            }
            return View(aulaPeriodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarAula(AulaPeriodoVM aulaPeriodoVM)
        {
            if (!ModelState.IsValid)
            {
                // El modelo no es válido, devuelve la vista con los errores
                return View(aulaPeriodoVM);
            }

            if (aulaPeriodoVM.Aula.Aula_Id == 0)
            {
                return View(aulaPeriodoVM.Aula);
            }
            else
            {
                objUsuario.Aula.Update(aulaPeriodoVM.Aula);
                objUsuario.SaveChanges();
                return RedirectToAction(nameof(ListaAulas));
            }
        }

        [HttpGet]
        public IActionResult BorrarAula(int? id)
        {
            var aula = objUsuario.Aula.FirstOrDefault(c => c.Aula_Id == id);
            objUsuario.Aula.Remove(aula);
            objUsuario.SaveChanges();
            return RedirectToAction(nameof(ListaAulas));
        }


        /*-------------------------------ADMINISTRAR ESTUDIANTES-------------------------------*/
        [HttpGet]
        public IActionResult AdministrarEstudiantes(int id)
        {
            CursoEstudianteVM cursoEstudiantes = new CursoEstudianteVM
            {
                ListaEstudiantesPorCurso = objUsuario.EstudiantesPorCursos.Include(e => e.Estudiante).
                Include(a => a.Curso).Where(a => a.Curso_Id == id),

                EstudiantesPorCurso = new EstudiantesPorCurso()
                {
                    Curso_Id = id
                },
                Curso = objUsuario.Curso.FirstOrDefault(a => a.Curso_Id == id)
            };

            List<int> listaTemporalEstudiantesCurso = cursoEstudiantes.ListaEstudiantesPorCurso.
                Select(e => e.Estudiante_Id).ToList();

            //Obtener todas las etiquetas cuyos id's no estén en la listaTemporal
            //Crear un NOT IN usando LINQ
            var listaTemporal = objUsuario.Estudiante.Where(e => !listaTemporalEstudiantesCurso.
            Contains(e.Estudiante_Id)).ToList();

            //Crear lista de etiquetas para el dropdown
            cursoEstudiantes.ListaEstudiantes = listaTemporal.Select(i => new SelectListItem
            {
                Text = $"{i.Estudiante_Apellido} {i.Estudiante_Nombre}",
                Value = i.Estudiante_Id.ToString()
            });

            return View(cursoEstudiantes);
        }
        
        /*
        [HttpPost]
        public IActionResult AdministrarEtiquetas(ArticuloEtiquetaVM articuloEtiquetas)
        {
            if (articuloEtiquetas.ArticuloEtiqueta.Articulo_Id != 0 &&
                articuloEtiquetas.ArticuloEtiqueta.Etiqueta_Id != 0)
            {
                _contexto.ArticuloEtiqueta.Add(articuloEtiquetas.ArticuloEtiqueta);
                _contexto.SaveChanges();
            }
            return RedirectToAction(nameof(AdministrarEtiquetas), new
            {
                @id = articuloEtiquetas.ArticuloEtiqueta.Articulo_Id
            });
        }

        [HttpPost]
        public IActionResult EliminarEtiquetas(int idEtiqueta, ArticuloEtiquetaVM articuloEtiquetas)
        {
            int idArticulo = articuloEtiquetas.Articulo.Articulo_Id;
            ArticuloEtiqueta articuloEtiqueta = _contexto.ArticuloEtiqueta.FirstOrDefault(
                    u => u.Etiqueta_Id == idEtiqueta && u.Articulo_Id == idArticulo
                );

            _contexto.ArticuloEtiqueta.Remove(articuloEtiqueta);
            _contexto.SaveChanges();

            return RedirectToAction(nameof(AdministrarEtiquetas), new { @id = idArticulo });
        }*/
    }
}
