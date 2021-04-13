using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Filter;
using WebClinica.Models;

namespace Clinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class TipoUsuariosController : Controller
    {
        List<TipoUsuario> listaTipoUsuarios = new List<TipoUsuario>();

        private readonly DBClinicaAcmeContext _db;
    
        public TipoUsuariosController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public List<TipoUsuario> listarTipoUsuarios()
        {
            listaTipoUsuarios = (from tipoUsuario in _db.TipoUsuario
                             select new TipoUsuario
                             {
                                 TipoUsuarioId = tipoUsuario.TipoUsuarioId,
                                 Nombre = tipoUsuario.Nombre,
                                 BotonHabilitado = tipoUsuario.BotonHabilitado,
                                 Descripcion = tipoUsuario.Descripcion
                             }).ToList();
            return listaTipoUsuarios;
        }

        private void cargarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<TipoUsuario>().OrderByDescending(
                t => t.TipoUsuarioId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;

            }
            else
            {
                ViewBag.ID = ultimoRegistro.TipoUsuarioId + 1;
            }
        }

        public IActionResult Index()
        {
            CargarUsuario();
            listaTipoUsuarios = listarTipoUsuarios();
            return View(listaTipoUsuarios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            cargarUltimoRegistro();
            return View();
        }

        [HttpPost]
        public IActionResult Create(TipoUsuario tipoUsuario)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid && tipoUsuario == null)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item list-group-item-danger'>";
                        rpta += item;
                        rpta += "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    cargarUltimoRegistro();
                    TipoUsuario _tipoUsuario = new TipoUsuario
                    {
                        TipoUsuarioId = ViewBag.ID,
                        Nombre = tipoUsuario.Nombre,
                        BotonHabilitado = 1,
                        Descripcion = tipoUsuario.Descripcion
                    };

                    _db.TipoUsuario.Add(_tipoUsuario);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            TipoUsuario oTipoUsuario = _db.TipoUsuario
                         .Where(e => e.TipoUsuarioId == id).First();
            return View(oTipoUsuario);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            TipoUsuario oTipoUsuario = _db.TipoUsuario
                         .Where(e => e.TipoUsuarioId == id).First();
            return View(oTipoUsuario);
        }

        [HttpPost]
        public IActionResult Edit(TipoUsuario tipoUsuario)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(tipoUsuario);
                }
                else
                {
                    TipoUsuario _tipoUsuario = new TipoUsuario
                    {
                        TipoUsuarioId = tipoUsuario.TipoUsuarioId,
                        Nombre = tipoUsuario.Nombre,
                        BotonHabilitado = tipoUsuario.BotonHabilitado,
                        Descripcion = tipoUsuario.Descripcion
                    };

                    _db.TipoUsuario.Update(_tipoUsuario);
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
        public IActionResult Delete(int? TipoUsuarioId)
        {
            var Error = "";
            try
            {
                TipoUsuario oTipoUsuario = _db.TipoUsuario
                             .Where(e => e.TipoUsuarioId == TipoUsuarioId).First();
                _db.TipoUsuario.Remove(oTipoUsuario);
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
