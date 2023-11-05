using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        
        public IActionResult Index()
        {
            var objEstudiante = JsonConvert.DeserializeObject<EstudiantesPorCurso>(HttpContext.Session.GetString("SUsuario"));
            var cursosEstudiante = _context.Curso.Where(e => e.Aula_Id == objEstudiante.EstudiantesPorCurso_Id).ToList();
            return View(cursosEstudiante);
        }
        /*        
        public IActionResult Index(int id)
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
        }*/
        public IActionResult EditarPerfil(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var estudiante = _context.Estudiante.FirstOrDefault(c => c.Estudiante_Id == id);
            return View(estudiante);
        }

        public IActionResult VerHorario()
        {
            
            return View();
        }

        public IActionResult VerEvaluaciones(int id)
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
        public IActionResult ListarEvaluacionesNota(Evaluacion eva)
        {
            var listaNotas = _context.EvaluacionPorEstudiantes
                .Where(e => e.EvaluacionPorEstudiante_Id == eva.Evaluacion_Id && e.Evaluacion.Curso_Id == eva.Curso.Curso_Id);

            return View(listaNotas);
        }
        public IActionResult VerAnuncio()
        {
            List<AnuncioInformativo> listaAnuncioInformativo = _context.AnuncioInformativo.ToList();
            return View(listaAnuncioInformativo);
        }
        
        public IActionResult SolicitarAsesoria(Asesoria asesoria)
        {
            _context.Asesorias.Add(asesoria);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        /*public IActionResult ListaSesiones(int id)
        {
            var curso = _context.Curso.Find(id);
            var listaSesiones = _context.Sesiones.Where(e => e.Curso_Id == id).ToList();
            var listaFinal = new SesionesViewModel()
            {
                Curso = curso,
                Sesiones = listaSesiones
            };
            return View(listaFinal);
        }*/

        /*public IActionResult AsistenciaPorSesion(int id)
        {
            var listaAsistencia = _context.EstudiantesPorSesions.Where(e => e.Sesion_Id == id).ToList();
            return View(listaAsistencia);
        }*/


    }
}

