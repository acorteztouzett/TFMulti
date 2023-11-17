using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinalMulti.Data;
using TrabajoFinalMulti.Models;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalMulti.ViewModel;

namespace TrabajoFinalMulti.Controllers
{
    public class DocenteController : Controller
    {
        public readonly ApplicationDbContext _context;

        public DocenteController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Verifica si hay un Docente en la sesión
            var docenteString = HttpContext.Session.GetString("SDocente");

            if (string.IsNullOrEmpty(docenteString))
            {
                // No hay información de Docente en la sesión, redirige a una vista de no encontrado
                return RedirectToAction("NoEncontrado");
            }

            // Hay información de Docente en la sesión, continúa con la lógica actual
            var objDocente = JsonConvert.DeserializeObject<Docente>(docenteString);
            var cursosDocente = _context.Curso.Where(e => e.Docente_Id == objDocente.Docente_Id).ToList();
            return View(cursosDocente);
        }

        public IActionResult NoEncontrado()
        {
            return View("~/Views/Shared/Error.cshtml");
        }


        public IActionResult DetalleCurso(int id)
        {
            var curso = _context.Curso.Find(id);
            return View(curso);
        }

        public IActionResult ListaSesiones(int id)
        {
            var curso = _context.Curso.Find(id);
            var listaSesiones = _context.Sesiones.Where(e => e.Curso_Id == id).ToList();
            var listaFinal = new SesionesViewModel()
            {
                Curso = curso,
                Sesiones = listaSesiones
            };

            return View(listaFinal);
        }

        public IActionResult Asistencia(int id)
        {
            var sesion = _context.Sesiones.Find(id);
            var listaAsistencia = _context.EstudiantesPorSesions.Include(e => e.Estudiante).AsNoTracking().Where(e => e.Sesion_Id == id).ToList();
            var lista = new AsistenciaViewModel()
            {
                Curso_Id = sesion.Curso_Id,
                Sesion = sesion,
                Estudiantes = listaAsistencia
            };
            return View(lista);
        }

        [HttpPost]
        public IActionResult RegistrarAsistencia(AsistenciaViewModel asistenciaView)
        {
            if (ModelState.IsValid)
            {
                //Console.WriteLine(asistenciaView.Estudiantes.Count);
                foreach (var asistencia in asistenciaView.Estudiantes)
                {
                    _context.EstudiantesPorSesions.Update(asistencia);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("ListaSesiones", new { id = asistenciaView.Curso_Id });
        }

        public IActionResult ListaEstudiantes(int id)
        {
            var listaEstudiantes = _context.EstudiantesPorSesions.Include(e => e.Estudiante).AsNoTracking().Where(e => e.Sesion_Id == id);

            return View(listaEstudiantes);
        }

        public IActionResult ListaEstudiantesCurso(int id)
        {
            var listaEstudiantes = _context.EstudiantesPorCursos.Include(e => e.Estudiante)
            .AsNoTracking()
            .Where(e => e.Curso_Id == id).ToList();

            return View(listaEstudiantes);
        }

        public IActionResult NotasEstudiante(int id)
        {
            var estudianteCurso = _context.EstudiantesPorCursos.Include(e => e.Estudiante).FirstOrDefault(e => e.EstudiantesPorCurso_Id == id);

            var evalucionesCurso = _context.Evaluaciones.Where(e => e.Curso_Id == estudianteCurso.Curso_Id).ToList();

            var evaluacionIds = evalucionesCurso.Select(ec => ec.Evaluacion_Id).ToList();

            var lista = _context.EvaluacionPorEstudiantes
                .Include(e => e.Estudiante)
                .Include(e => e.Evaluacion)
                .AsNoTracking()
                .Where(e => evaluacionIds.Contains(e.Evaluacion_Id) && e.Estudiante_Id == estudianteCurso.Estudiante_Id)
                .ToList();

            NotasViewModel notasView = new()
            {
                Curso_Id = estudianteCurso.Curso_Id,
                Estudiante_Id = estudianteCurso.Estudiante_Id,
                Estudiante_Nombre = estudianteCurso.Estudiante.Estudiante_Nombre,
                NotasEstudiante = lista
            };

            return View(notasView);
        }

        public IActionResult RegistrarNotas(NotasViewModel notasView)
        {
            if (ModelState.IsValid)
            {
                //Console.WriteLine(asistenciaView.Estudiantes.Count);
                foreach (var asistencia in notasView.NotasEstudiante)
                {
                    _context.EvaluacionPorEstudiantes.Update(asistencia);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("ListaEstudiantesCurso", new { id = notasView.Curso_Id });
        }

        public IActionResult ListaEvaluaciones(int id)
        {
            var curso = _context.Curso.Find(id);
            var listaEvaluaciones = _context.Evaluaciones.Where(e => e.Curso_Id == id);

            var listaCompleta = new ListaEvaluacionesViewModel()
            {
                Curso = curso,
                Evaluaciones = listaEvaluaciones
            };

            return View(listaCompleta);
        }

        public IActionResult CrearEvaluacion(int id)
        {
            var evaluacion = new RegistroEvaluacion
            {
                Curso_Id = id // Asignar el valor de id al modelo Evaluacion
            };
            return View(evaluacion);
        }

        public IActionResult NuevaEvaluacion(RegistroEvaluacion evaluacion)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(evaluacion.Curso_Id);
                Evaluacion evaluacion1 = new()
                {
                    Curso_Id = evaluacion.Curso_Id,
                    Nombre = evaluacion.Nombre,
                    Fecha = evaluacion.Fecha
                };
                _context.Evaluaciones.Add(evaluacion1);
                _context.SaveChanges();

                var listaEstudiantes = _context.EstudiantesPorCursos.Where(e => e.Curso_Id == evaluacion.Curso_Id).ToList();
                foreach (var est in listaEstudiantes)
                {
                    EvaluacionPorEstudiante estud = new()
                    {
                        Nota = 0,
                        Evaluacion_Id = evaluacion1.Evaluacion_Id,
                        Estudiante_Id = est.Estudiante_Id
                    };

                    _context.EvaluacionPorEstudiantes.Add(estud);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("ListaEvaluaciones", new { id = evaluacion.Curso_Id });
        }

        public IActionResult ListarEvaluacionesPorAlumno(Evaluacion evu)
        {
            var listaAlumnos = _context.EvaluacionPorEstudiantes
                .Where(e => e.Evaluacion_Id == evu.Evaluacion_Id && e.Evaluacion.Curso_Id == evu.Curso.Curso_Id);

            return View(listaAlumnos);
        }


        /*-----------------------------HORARIO DE DOCENTE------------------------------*/
        [HttpGet]
        public IActionResult VerHorarioDocente()
        {
            var objDocente = JsonConvert.DeserializeObject<Docente>(HttpContext.Session.GetString("SDocente"));

            // Obtén los cursos del docente
            var cursosDocente = _context.Curso
                .Where(c => c.Docente_Id == objDocente.Docente_Id)
                .ToList();

            // Obtén los horarios para cada curso del docente
            var horariosDocente = new List<Horario>();
            foreach (var curso in cursosDocente)
            {
                var horarios = _context.Horario
                    .Where(h => h.Curso_Id == curso.Curso_Id)
                    .ToList();

                horariosDocente.AddRange(horarios);
            }

            // Pasa los datos del horario a la vista junto con el nombre del docente
            ViewData["DocenteNombre"] = $"{objDocente.Docente_Nombre} {objDocente.Docente_Apellido}";

            return View(horariosDocente);
        }


    }
}