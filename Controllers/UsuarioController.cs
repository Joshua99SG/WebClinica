using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Models;
using WebClinica.Models.ViewModel;

namespace WebClinica.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<UsuarioTipoUsuario> listaUsuario = new List<UsuarioTipoUsuario>();
        public UsuarioController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaUsuario = (from usuario in _db.Usuario
                            join tipoUsuario in _db.TipoUsuario
                            on usuario.UsuarioId equals tipoUsuario.TipoUsuarioId
                             select new UsuarioTipoUsuario
                             {
                                 UsuarioId = usuario.UsuarioId,
                                 TipoUsuarioNombre = tipoUsuario.Nombre,
                                 Nombre = usuario.Nombre,
                                 Password = usuario.Password
                             }).ToList();
            var model = listaUsuario;
            return View("Index", model);
        }

        private void cargarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Usuario>().OrderByDescending(e => e.UsuarioId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;
            }
            else
            {
                ViewBag.ID = ultimoRegistro.UsuarioId + 1;
            }
        }

        private void cargarTipoUsuarios()
        {
            List<SelectListItem> listaTipoUsuario = new List<SelectListItem>();
            listaTipoUsuario = (from tipoUsuario in _db.TipoUsuario
                                orderby  tipoUsuario.Nombre
                                select new SelectListItem
                                { 
                                Text = tipoUsuario.Nombre,
                                Value = tipoUsuario.TipoUsuarioId.ToString()
                                }).ToList();
            ViewBag.ListaTipoUsuario = listaTipoUsuario;
        }

        [HttpGet]
        public IActionResult Create()
        {
            cargarTipoUsuarios();
            cargarUltimoRegistro();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(usuario);
                }
                else
                {
                    _db.Usuario.Add(usuario);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            Usuario usuario = _db.Usuario
                         .Where(e => e.UsuarioId == id).First();
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Usuario usuario = _db.Usuario
                         .Where(e => e.UsuarioId == id).First();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Edit(Usuario usuario)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(usuario);
                }
                else
                {
                    _db.Usuario.Update(usuario);
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
        public IActionResult Delete(int id)
        {
            var Error = "";
            try
            {
                Usuario usuario = _db.Usuario
                             .Where(e => e.UsuarioId == id).First();
                _db.Usuario.Remove(usuario);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
