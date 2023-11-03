﻿using Microsoft.AspNetCore.Mvc;
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
        public readonly ApplicationDbContext _context;

        public AdministradorController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
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
                if (_context.Docente.Any(d => d.Docente_Correo == viewModel.Correo) ||
                    _context.Estudiante.Any(e => e.Estudiante_Correo == viewModel.Correo))
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

                    if (viewModel.TipoUsuario == "docente" && _context.Docente.Any())
                    {
                        nuevoId = _context.Docente.Max(d => d.Docente_Id) + 1;
                    }
                    else if (viewModel.TipoUsuario == "estudiante" && _context.Estudiante.Any())
                    {
                        nuevoId = _context.Estudiante.Max(e => e.Estudiante_Id) + 1;
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
                        _context.Docente.Add(docente);
                        _context.SaveChanges();

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
                        _context.Estudiante.Add(estudiante);
                        _context.SaveChanges();

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
            List<Docente> listaDocentes = _context.Docente.ToList();
            return View(listaDocentes);
        }

        [HttpGet]
        public IActionResult EditarDocente(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var docente = _context.Docente.FirstOrDefault(c => c.Docente_Id == id);
            return View(docente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarDocente(Docente docente, IFormFile nuevaFoto)
        {
            if (ModelState.IsValid)
            {
                if (_context.Docente.Any(d => d.Docente_Correo == docente.Docente_Correo && d.Docente_Id != docente.Docente_Id))
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
                    _context.Docente.Update(docente);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(ListaDocentes));
                }
            }

            return View(docente);
        }


        [HttpGet]
        public IActionResult EliminarDocente(int? id)
        {
            var docente = _context.Docente.FirstOrDefault(c => c.Docente_Id == id);
            _context.Docente.Remove(docente);
            _context.SaveChanges();
            return RedirectToAction("ListaDocentes", "Administrador");
        }

        /*------------------------ESTUDIANTES--------------------------------------*/
        //LISTAR ESTUDIANTES:
        public IActionResult ListaEstudiantes()
        {
            //List<Estudiante> listaEstudiantes = _context.Estudiante.ToList();
            List<Estudiante> listaEstudiantes = _context.Estudiante
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
            var estudiante = _context.Estudiante.FirstOrDefault(c => c.Estudiante_Id == id);
            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarEstudiante(Estudiante estudiante, IFormFile nuevaFoto)
        {
            if (ModelState.IsValid)
            {
                if (_context.Estudiante.Any(d => d.Estudiante_Correo == estudiante.Estudiante_Correo && d.Estudiante_Id != estudiante.Estudiante_Id))
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
                    _context.Estudiante.Update(estudiante);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(ListaEstudiantes));
                }
            }

            return View(estudiante);
        }

        [HttpGet]
        public IActionResult EliminarEstudiante(int? id)
        {
            var estudiante = _context.Estudiante.FirstOrDefault(c => c.Estudiante_Id == id);
            _context.Estudiante.Remove(estudiante);
            _context.SaveChanges();
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
            var estudiante = _context.Estudiante.Include(d => d.Apoderado).FirstOrDefault(u => u.Estudiante_Id == id);
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
                _context.Apoderado.Add(estudiante.Apoderado);
                _context.SaveChanges();

                // Actualiza la referencia del apoderado en el estudiante
                var estudianteBd = _context.Estudiante.FirstOrDefault(u => u.Estudiante_Id == estudiante.Estudiante_Id);
                estudianteBd.Apoderado_Id = estudiante.Apoderado.Apoderado_Id;
                _context.SaveChanges();             
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
            var apoderado = _context.Apoderado.FirstOrDefault(c => c.Apoderado_Id == id);
            return View(apoderado);
        }

        [HttpPost]
        public IActionResult EditarApoderado(Apoderado apoderado)
        {
            if (ModelState.IsValid)
            {
                _context.Apoderado.Update(apoderado);
                _context.SaveChanges();
                return RedirectToAction(nameof(ListaEstudiantes));
            }

            return View(apoderado);
        }

        [HttpGet]
        public IActionResult EliminarApoderado(int? id)
        {
            var apoderado = _context.Apoderado.FirstOrDefault(c => c.Apoderado_Id == id);

            if (apoderado != null)
            {
                // Obten los estudiantes relacionados y establece Apoderado_Id a null
                var estudiantesRelacionados = _context.Estudiante.Where(e => e.Apoderado_Id == id);
                foreach (var estudiante in estudiantesRelacionados)
                {
                    estudiante.Apoderado_Id = null;
                }

                _context.Apoderado.Remove(apoderado);
                _context.SaveChanges();
            }

            return RedirectToAction("ListaEstudiantes", "Administrador");
        }


        /*------------------------BUSQUEDA EN DOCENTES Y ESTUDIANTES--------------------------------------*/
        [HttpGet]
        public IActionResult BuscarDocente(string term)
        {
            // Realiza la búsqueda en tu base de datos y obtiene los resultados
            var resultados = _context.Docente
                .Where(u => u.Docente_Nombre.Contains(term))
                .ToList();

            return PartialView("_TablaDocentes", resultados);
        }

        [HttpGet]
        public IActionResult BuscarEstudiante(string term)
        {
            // Realiza la búsqueda en tu base de datos y obtiene los resultados
            var resultados = _context.Estudiante
                .Where(u => u.Estudiante_Nombre.Contains(term))
                .ToList();

            return PartialView("_TablaEstudiantes", resultados);
        }


        /*------------------------CREAR CURSOS Y ASIGNARLOS A UN DOCENTE--------------------------------------*/
        /*public IActionResult ListaCursos()
        {
            List<Curso> listaCursos = _context.Curso.ToList();
            return View(listaCursos);
        }*/

        public IActionResult ListaCursos()
        {
            var cursos = _context.Curso
                .Include(c => c.Docente) // Esto carga el docente relacionado
                .Include(c => c.Aula)
                .ToList();

            return View(cursos);
        }

        [HttpGet]
        public IActionResult RegistrarCurso()
        {
            CursoDocenteVM cursoDocenteAula = new CursoDocenteVM();
            cursoDocenteAula.ListaDocentes = _context.Docente.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = $"{i.Docente_Nombre} {i.Docente_Apellido}",
                Value = i.Docente_Id.ToString()
            });

            cursoDocenteAula.ListaAulas = _context.Aula.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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
                _context.Curso.Add(curso);
                _context.SaveChanges();
                return RedirectToAction(nameof(ListaCursos));
            }

            CursoDocenteVM cursoDocenteAula = new CursoDocenteVM();
            cursoDocenteAula.ListaDocentes = _context.Docente.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = $"{i.Docente_Nombre} {i.Docente_Apellido}",
                Value = i.Docente_Id.ToString()
            });

            cursoDocenteAula.ListaAulas = _context.Aula.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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

            CursoDocenteVM cursoDocenteAula = new CursoDocenteVM();
            cursoDocenteAula.ListaDocentes = _context.Docente.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = $"{i.Docente_Nombre} {i.Docente_Apellido}",
                Value = i.Docente_Id.ToString()
            });

            cursoDocenteAula.ListaAulas = _context.Aula.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = $"{i.Aula_NivelEducativo}  {i.Aula_Grado}",
                Value = i.Aula_Id.ToString()
            });


            cursoDocenteAula.Curso = _context.Curso.FirstOrDefault(c => c.Curso_Id == id);
            if (cursoDocenteAula == null)
            {
                return NotFound();
            }
            return View(cursoDocenteAula);
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
                _context.Curso.Update(cursoDocenteVM.Curso);
                _context.SaveChanges();
                return RedirectToAction(nameof(ListaCursos));
            }
        }

        [HttpGet]
        public IActionResult BorrarCurso(int? id)
        {
            var curso = _context.Curso.FirstOrDefault(c => c.Curso_Id == id);
            _context.Curso.Remove(curso);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListaCursos));
        }

        /*------------------------ANUNCIOS INFORMATIVOS--------------------------------------*/
        public IActionResult ListaAnuncioInformativo()
        {
            List<AnuncioInformativo> listaAnuncioInformativo = _context.AnuncioInformativo.ToList();
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
                _context.AnuncioInformativo.Add(anuncio);
                _context.SaveChanges();
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
            var anuncio = _context.AnuncioInformativo.FirstOrDefault(c => c.Anuncio_Id == id);
            return View(anuncio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarAnuncioInformativo(AnuncioInformativo anuncio)
        {
            if (ModelState.IsValid)
            {
                _context.AnuncioInformativo.Update(anuncio);
                _context.SaveChanges();
                return RedirectToAction(nameof(ListaAnuncioInformativo));
            }
            return View(anuncio);
        }

        [HttpGet]
        public IActionResult BorrarAnuncioInformativo(int? id)
        {
            var anuncio = _context.AnuncioInformativo.FirstOrDefault(c => c.Anuncio_Id == id);
            _context.AnuncioInformativo.Remove(anuncio);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListaAnuncioInformativo));
        }

        /*------------------------PERIODO--------------------------------------*/
        public IActionResult ListaPeriodos()
        {
            List<Periodo> listaPeriodos = _context.Periodo.OrderBy(p => p.Periodo_Año).ToList();
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
                _context.Periodo.Add(periodo);
                _context.SaveChanges();
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
            var periodo = _context.Periodo.FirstOrDefault(c => c.Periodo_Id == id);
            return View(periodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPeriodo(Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                _context.Periodo.Update(periodo);
                _context.SaveChanges();
                return RedirectToAction(nameof(ListaPeriodos));
            }
            return View(periodo);
        }

        [HttpGet]
        public IActionResult BorrarPeriodo(int? id)
        {
            var periodo = _context.Periodo.FirstOrDefault(c => c.Periodo_Id == id);
            _context.Periodo.Remove(periodo);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListaPeriodos));
        }


        /*------------------------CREAR AULA Y ASIGNARLO A UN PERIODO--------------------------------------*/
        public IActionResult ListaAulas()
        {
            var aulas = _context.Aula.Include(p => p.Periodo).ToList();

            return View(aulas);
        }

        [HttpGet]
        public IActionResult RegistrarAula()
        {
            AulaPeriodoVM aulaPeriodo = new AulaPeriodoVM();
            aulaPeriodo.ListaPeriodos = _context.Periodo.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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
                _context.Aula.Add(aula);
                _context.SaveChanges();
                return RedirectToAction(nameof(ListaAulas));
            }
            AulaPeriodoVM aulaPeriodo = new AulaPeriodoVM();
            aulaPeriodo.ListaPeriodos = _context.Periodo.Select(i => new SelectListItem
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
            aulaPeriodo.ListaPeriodos = _context.Periodo.Select(i => new SelectListItem
            {
                Text = i.Periodo_Año,
                Value = i.Periodo_Id.ToString()
            });

            aulaPeriodo.Aula = _context.Aula.FirstOrDefault(c => c.Aula_Id == id);
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
                _context.Aula.Update(aulaPeriodoVM.Aula);
                _context.SaveChanges();
                return RedirectToAction(nameof(ListaAulas));
            }
        }

        [HttpGet]
        public IActionResult BorrarAula(int? id)
        {
            var aula = _context.Aula.FirstOrDefault(c => c.Aula_Id == id);
            _context.Aula.Remove(aula);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListaAulas));
        }


        /*-------------------------------ADMINISTRAR ESTUDIANTES-------------------------------*/
        /*[HttpGet]
        public IActionResult AdministrarEstudiantes(int id)
        {
            CursoEstudianteVM cursoEstudiantes = new CursoEstudianteVM
            {
                ListaEstudiantesPorCurso = _context.EstudiantesPorCursos.Include(e => e.Estudiante).
                Include(a => a.Curso).Where(a => a.Curso_Id == id),

                EstudiantesPorCurso = new EstudiantesPorCurso()
                {
                    Curso_Id = id
                },
                Curso = _context.Curso.FirstOrDefault(a => a.Curso_Id == id)
            };

            List<int> listaTemporalEstudiantesCurso = cursoEstudiantes.ListaEstudiantesPorCurso.
                Select(e => e.Estudiante_Id).ToList();

            //Obtener todas las etiquetas cuyos id's no estén en la listaTemporal
            //Crear un NOT IN usando LINQ
            var listaTemporal = _context.Estudiante.Where(e => !listaTemporalEstudiantesCurso.
            Contains(e.Estudiante_Id)).ToList();

            //Crear lista de etiquetas para el dropdown
            cursoEstudiantes.ListaEstudiantes = listaTemporal.Select(i => new SelectListItem
            {
                Text = $"{i.Estudiante_Apellido} {i.Estudiante_Nombre}",
                Value = i.Estudiante_Id.ToString()
            });

            return View(cursoEstudiantes);
        }*/

        [HttpGet]
        public IActionResult AdministrarEstudiantes(int id)
        {
            Curso curso = _context.Curso.Include(c => c.Aula).FirstOrDefault(a => a.Curso_Id == id);

            if (curso != null)
            {
                CursoEstudianteVM cursoEstudiantes = new CursoEstudianteVM
                {
                    ListaEstudiantesPorCurso = _context.EstudiantesPorCursos.Include(e => e.Estudiante)
                        .Include(a => a.Curso).Where(a => a.Curso_Id == id),

                    EstudiantesPorCurso = new EstudiantesPorCurso()
                    {
                        Curso_Id = id
                    },
                    Curso = curso,
                    Aforo = curso.Aula != null ? curso.Aula.Aula_Aforo : 0 // Obtener el aforo del aula
                };

                List<int> listaTemporalEstudiantesCurso = cursoEstudiantes.ListaEstudiantesPorCurso
                    .Select(e => e.Estudiante_Id).ToList();

                // Obtener todas las etiquetas cuyos ID no estén en la listaTemporal
                // Crear un NOT IN usando LINQ
                var listaTemporal = _context.Estudiante.Where(e => !listaTemporalEstudiantesCurso
                    .Contains(e.Estudiante_Id)).ToList();

                // Crear lista de etiquetas para el dropdown
                cursoEstudiantes.ListaEstudiantes = listaTemporal.Select(i => new SelectListItem
                {
                    Text = $"{i.Estudiante_Apellido} {i.Estudiante_Nombre}",
                    Value = i.Estudiante_Id.ToString()
                });

                return View(cursoEstudiantes);
            }
            else
            {
                // Manejar el caso en el que no se encuentra el curso
                return NotFound(); // Puedes personalizar esto según tus necesidades
            }
        }

        [HttpPost]
        public IActionResult AdministrarEstudiantes(CursoEstudianteVM cursoEstudiantes)
        {

            if (cursoEstudiantes.EstudiantesPorCurso.Curso_Id != 0 &&
                cursoEstudiantes.EstudiantesPorCurso.Estudiante_Id != 0)
            {
                _context.EstudiantesPorCursos.Add(cursoEstudiantes.EstudiantesPorCurso);
                _context.SaveChanges();

            }
            return RedirectToAction(nameof(AdministrarEstudiantes), new
            {
                @id = cursoEstudiantes.EstudiantesPorCurso.Curso_Id
            });
        }


        [HttpPost]
        public IActionResult EliminarEstudiantes(int idEstudiante, CursoEstudianteVM cursoEstudiantes)
        {
            int idCurso = cursoEstudiantes.Curso.Curso_Id;

            EstudiantesPorCurso cursoEstudiante = _context.EstudiantesPorCursos.FirstOrDefault(
                u => u.Estudiante_Id == idEstudiante && u.Curso_Id == idCurso
            );

            if (cursoEstudiante != null)
            {
                _context.EstudiantesPorCursos.Remove(cursoEstudiante);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(AdministrarEstudiantes), new { id = idCurso });
        }

    }
}
