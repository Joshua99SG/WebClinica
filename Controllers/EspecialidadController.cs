
using Clinica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using WebClinica.Filter;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class EspecialidadController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<Especialidad> listaEspecialidad = new List<Especialidad>();
        public EspecialidadController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            listaEspecialidad = (from especialidad in _db.Especialidad
                                 select new Especialidad
                                 {
                                     EspecialidadId = especialidad.EspecialidadId,
                                     Nombre = especialidad.Nombre,
                                     Descripcion = especialidad.Descripcion.Substring(0, 85) + "..."  //Cambio
                                 }).ToList();
            CargarUsuario();
            var model = listaEspecialidad;
            return View("Index", model);
        }

        private void cargarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Especialidad>().OrderByDescending(e => e.EspecialidadId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;
            }
            else
            {
                ViewBag.ID = ultimoRegistro.EspecialidadId + 1;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            cargarUltimoRegistro();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Especialidad especialidad)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(especialidad);
                }
                else
                {
                    cargarUltimoRegistro();
                    Especialidad _especialidad = new Especialidad();
                    _especialidad.EspecialidadId = ViewBag.ID;
                    _especialidad.Nombre = especialidad.Nombre;
                    _especialidad.Descripcion = especialidad.Descripcion;
                    _db.Especialidad.Add(_especialidad);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        //Apartir de aca todo nuevo
        public IActionResult Details(int id)
        {
            Especialidad oEspecialidad = _db.Especialidad
                         .Where(e => e.EspecialidadId == id).First();
            return View(oEspecialidad);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Especialidad oEspecialidad = _db.Especialidad
                         .Where(e => e.EspecialidadId == id).First();
            return View(oEspecialidad);
        }

        [HttpPost]
        public IActionResult Edit(Especialidad especialidad)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(especialidad);
                }
                else
                {
                    _db.Especialidad.Update(especialidad);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int? EspecialidadId)
        {
            var Error = "";
            try
            {
                Especialidad oEspecialidad = _db.Especialidad
                             .Where(e => e.EspecialidadId == EspecialidadId).First();
                _db.Especialidad.Remove(oEspecialidad);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public void CargarUsuario()
        {
            ViewBag.Usuario = HttpContext.Session.GetString("nombreUsuario");
        }
    }
}
