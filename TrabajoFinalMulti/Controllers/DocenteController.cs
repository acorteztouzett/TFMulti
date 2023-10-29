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
            var objDocente = JsonConvert.DeserializeObject<Docente>(HttpContext.Session.GetString("SDocente"));
            var cursosDocente = _context.Curso.Where(e => e.Docente_Id == objDocente.Docente_Id).ToList();
            return View(cursosDocente);
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

        public  IActionResult Asistencia(int id)
        {
            var sesion = _context.Sesiones.Find(id);
            var listaAsistencia = _context.EstudiantesPorSesions.Include(e =>e.Estudiante).AsNoTracking().Where(e => e.Sesion_Id == id).ToList();
            var lista = new AsistenciaViewModel()
            {
                Sesion = sesion,
                Estudiantes = listaAsistencia
            };
            return View(lista);
        }

        public IActionResult ListaEstudiantes(int id)
        {
            var listaEstudiantes = _context.EstudiantesPorSesions.Include(e => e.Estudiante).AsNoTracking().Where(e => e.Sesion_Id == id);

            return View(listaEstudiantes);
        }

        public IActionResult EditarEvaluacion(int id)
        {
            return View();
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
            }

            return RedirectToAction("ListaEvaluaciones", new { id = evaluacion.Curso_Id });
        }

        public IActionResult ListarEvaluacionesPorAlumno(Evaluacion evu)
        {
            var listaAlumnos = _context.EvaluacionPorEstudiantes
                .Where(e => e.EvaluacionPorEstudiante_Id == evu.Evaluacion_Id && e.Evaluacion.Curso_Id == evu.Curso.Curso_Id);

            return View(listaAlumnos);
        }
    }
}
