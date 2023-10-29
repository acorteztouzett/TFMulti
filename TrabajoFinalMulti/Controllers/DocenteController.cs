using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalMulti.Data;
using TrabajoFinalMulti.Models;

namespace TrabajoFinalMulti.Controllers
{
    public class DocenteController : Controller
    {
        public readonly ApplicationDbContext _context;

        public DocenteController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index(int id)
        {
            var cursosDocente = _context.Curso.Where(e => e.Docente_Id == id ).ToList();
            return View(cursosDocente);
        }

        public IActionResult Detail(int id)
        {
            var curso = _context.Curso.Find(id);
            return View(curso);
        }

        public IActionResult ListaSesiones(int id)
        {
            var listaSesiones = _context.Sesiones.Where(e => e.Curso_Id == id).ToList();
    
            return View(listaSesiones);
        }

        public IActionResult ListaEstudiantes(int id)
        {
            var listaEstudiantes = _context.EstudiantesPorSesions.Include(e => e.Estudiante).AsNoTracking().Where(e => e.Sesion_Id == id);

            return View(listaEstudiantes);
        }

        public IActionResult listaEvaluaciones(int id)
        {
            var listaEvaluaciones = _context.Evaluaciones.Where(e => e.Curso_Id == id);
            return View(listaEvaluaciones);
        }

        public IActionResult nuevaEvaluacion(Evaluacion evaluacion)
        {
            _context.Evaluaciones.Add(evaluacion);
            _context.SaveChanges();
            return  View();
        }

        public IActionResult listarEvaluacionesPorAlumno(Evaluacion evu)
        {
            var listaAlumnos = _context.EvaluacionPorEstudiantes
                .Where(e => e.EvaluacionPorEstudiante_Id == evu.Evaluacion_Id && e.Evaluacion.Curso_Id == evu.Curso.Curso_Id);

            return View(listaAlumnos);
        }
    }
}
