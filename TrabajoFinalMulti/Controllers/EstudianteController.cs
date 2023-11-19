using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using TrabajoFinalMulti.Data;
using TrabajoFinalMulti.Models;
using TrabajoFinalMulti.ViewModel;

namespace TrabajoFinalMulti.Controllers
{
    public class EstudianteController : Controller
    {
        public readonly ApplicationDbContext _context;

        public EstudianteController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var estudianteString = HttpContext.Session.GetString("SUsuario");

            if (string.IsNullOrEmpty(estudianteString))
            {
                // No hay información de estudiante en la sesión, redirige a una vista de no encontrado
                return RedirectToAction("NoEncontrado");
            }

            var objEstudiante = JsonConvert.DeserializeObject<EstudiantesPorCurso>(estudianteString);

            // Obtén los cursos del estudiante desde la tabla de unión
            var cursosEstudiante = _context.EstudiantesPorCursos
                .Where(e => e.Estudiante_Id == objEstudiante.Estudiante_Id)
                .Select(e => e.Curso)
                .ToList();

            return View(cursosEstudiante);
        }

        public IActionResult NoEncontrado()
        {
            return View("~/Views/Shared/Error.cshtml");
        }


        public IActionResult DetallePorCurso(int id)
        {
            var curso = _context.Curso.Find(id);
            return View(curso);
        }
        public IActionResult ListaCursos(int id)
        {
            var curso = _context.Curso.Find(id);
            var listaCursos = _context.Sesiones.Where(e => e.Curso_Id == id).ToList();
            var listaFinal = new SesionesViewModel()
            {
                Curso = curso,
                Sesiones = listaCursos
            };
            Console.WriteLine(listaFinal);
            return View(listaFinal);
        }
        public IActionResult ListaEvaluaciones(int id)
        {
            var curso = _context.Curso.Find(id);
            var listaEvaluaciones = _context.Evaluaciones.Where(e => e.Curso_Id == id);
            var listaNotas= _context.EvaluacionPorEstudiantes.Where(e => e.Evaluacion.Curso_Id == id);
            var listaCompleta = new ListaEvaluacionesViewModel()
            {
                Curso = curso,
                Evaluaciones = listaEvaluaciones,
                Notas= listaNotas
            };
            return View(listaCompleta);
        }
        public IActionResult ListarEvaluacionesNota(Evaluacion eva)
        {
            var listaNotas = _context.EvaluacionPorEstudiantes
                .Where(e => e.EvaluacionPorEstudiante_Id == eva.Evaluacion_Id && e.Evaluacion.Curso_Id == eva.Curso.Curso_Id);

            return View(listaNotas);
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
        public IActionResult AsistenciaPorSesion(int id)
        {
            var listaAsistencia = _context.EstudiantesPorSesions.Where(e => e.Sesion_Id == id).ToList();
            return View(listaAsistencia);
        }

        public IActionResult SolicitarAsesoria(int id)
        {
            var curso = _context.Curso.Find(id);
            var listaAsesorias = _context.Asesorias.Where(e => e.Curso_Id == id);

            var listaCompleta = new AsesoriaViewModel()
            {
                Curso = curso,
                Asesorias = listaAsesorias
            };

            return View(listaCompleta);
        }
        public IActionResult CrearAsesoria(int id)
        {
            var asesoria = new RegistroAsesoria
            {
                Curso_Id = id // Asignar el valor de id al modelo Evaluacion
            };
            return View(asesoria);
        }

        public IActionResult NuevaAsesoria(RegistroAsesoria asesoria)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(asesoria.Curso_Id);
                Asesoria asesoria1 = new()
                {
                    Curso_Id = asesoria.Curso_Id,
                    Tema = asesoria.Tema,
                    Fecha = asesoria.Fecha
                };
                _context.Asesorias.Add(asesoria1);
                _context.SaveChanges();
            }

            return RedirectToAction("SolicitarAsesoria", new { id = asesoria.Curso_Id });
        }

        /*-----------------------------HORARIO DE ESTUDIANTE------------------------------*/
        [HttpGet]
        public IActionResult VerHorario()
        {

            // Obtén el ID del estudiante desde la sesión
            var objEstudiante = JsonConvert.DeserializeObject<EstudiantesPorCurso>(HttpContext.Session.GetString("SUsuario"));
            var estudianteIdVerHorario = objEstudiante.Estudiante_Id;

            // Obtén los cursos del estudiante desde la tabla de unión
            var cursosEstudiante = _context.EstudiantesPorCursos
                .Where(e => e.Estudiante_Id == estudianteIdVerHorario)
                .Select(e => e.Curso)
                .ToList();

            // Obtén los horarios para cada curso
            var horariosDelEstudiante = new List<Horario>();
            foreach (var curso in cursosEstudiante)
            {
                var horarios = _context.Horario
                    .Where(h => h.Curso_Id == curso.Curso_Id)
                    .ToList();

                horariosDelEstudiante.AddRange(horarios);
            }

            return View(horariosDelEstudiante);
        }
        public IActionResult VerAsistencia(int id)
        {
            var curso = _context.Curso.Find(id);
            //Sesiones debe hacer un left join con EstudiantesPorSesiones

            var ListaSesiones = _context.Sesiones.Where(e => e.Curso_Id == id).ToList();
            var idSesion = ListaSesiones.Select(e => e.Sesiones_Id).ToList();
            var listaAsistencia= _context.EstudiantesPorSesions.Where(e => idSesion.Contains(e.Sesion_Id)).ToList();
            
            var listaFinal = new SesionesViewModel()
            {
                Curso = curso,
                Sesiones=ListaSesiones,
                Asistencia = listaAsistencia
            };

            return View(listaFinal);
        }

        public IActionResult EditarPerfil()
        {
            var objEstudiante = JsonConvert.DeserializeObject<EstudiantesPorCurso>(HttpContext.Session.GetString("SUsuario"));
            var estudianteId = objEstudiante.Estudiante_Id;

            var estudiante = _context.Estudiante.Find(estudianteId);

            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPerfil(Estudiante estudiante,IFormFile nuevaFoto)
        {
            if (ModelState.IsValid)
            {
                _context.Estudiante.Update(estudiante);
                _context.SaveChanges();
            }
          
            return RedirectToAction("Index", "Estudiante");
        }
    }
}