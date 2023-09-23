﻿using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinalMulti.Data;
using TrabajoFinalMulti.Models;
using TrabajoFinalMulti.ViewModel;

namespace TrabajoFinalMulti.Controllers
{
    public class UsuarioController : Controller
    {
        public readonly ApplicationDbContext objUsuario;

        public UsuarioController(ApplicationDbContext dbContext)
        {
            objUsuario = dbContext;
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
                var admin = objUsuario.Administrador.SingleOrDefault(a => a.Admin_Correo == viewModel.Correo && a.Admin_Contraseña == viewModel.Contraseña);

                // Verificar las credenciales ingresadas en la tabla de Docente
                var docente = objUsuario.Docente.SingleOrDefault(d => d.Docente_Correo == viewModel.Correo && d.Docente_Contraseña == viewModel.Contraseña);

                // Verificar las credenciales ingresadas en la tabla de Estudiante
                var estudiante = objUsuario.Estudiante.SingleOrDefault(e => e.Estudiante_Correo == viewModel.Correo && e.Estudiante_Contraseña == viewModel.Contraseña);

                // Comprobar si las credenciales coinciden en alguna de las tablas
                if (admin != null)
                {
                    // Credenciales válidas para Administrador, redirigir a la página correspondiente
                    return RedirectToAction("RegistrarUsuario");
                }
                else if (docente != null)
                {
                    // Credenciales válidas para Docente, redirigir a la página correspondiente
                    return RedirectToAction("Index", "Home");
                }
                else if (estudiante != null)
                {
                    // Credenciales válidas para Estudiante, redirigir a la página correspondiente
                    HttpContext.Session.SetString("SUsuario", JsonConvert.SerializeObject(estudiante));
                    return RedirectToAction("Privacy", "Home");
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
            var estudiante = objUsuario.Estudiante.SingleOrDefault(e => e.Estudiante_Id == model);
            return View(estudiante);
        }

        //CERRAR SESIÓN
        [HttpPost]
        public IActionResult Logout()
        {
            // Redirige al usuario a la página de inicio o a donde desees después de cerrar sesión.
            return RedirectToAction("Login");
        }

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
                    return View(viewModel);
                }

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
                }

                objUsuario.SaveChanges();
                return RedirectToAction("RegistrarUsuario");
            }
            return View(viewModel);
        }

        public IActionResult ActualizarUsuario(ActualizarViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var objeto = JsonConvert.DeserializeObject<Estudiante>(HttpContext.Session.GetString("SUsuario"));
                var estudiante = objUsuario.Estudiante.SingleOrDefault(a => a.Estudiante_Id == objeto.Estudiante_Id);
                estudiante.Estudiante_Nombre = viewmodel.Nombre;
                estudiante.Estudiante_Correo = viewmodel.Correo;
                estudiante.Estudiante_Contraseña = viewmodel.Contraseña;

                objUsuario.Estudiante.Update(estudiante);
                objUsuario.SaveChanges();

                HttpContext.Session.Clear();
                HttpContext.Session.SetString("SUsuario", JsonConvert.SerializeObject(estudiante));
            }
            return RedirectToAction("Privacy", "Home");
        }

    }
}
