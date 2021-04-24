using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Filter;
using WebClinica.Models;
using WebClinica.Models.ViewModel;

namespace Clinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class PaginaController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<Pagina> listaPagina = new List<Pagina>();
        private List<MenuController> ListaMenu = new List<MenuController>();
        public PaginaController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        private void CargarMenus()
        {
            List<SelectListItem> listaMenus = new List<SelectListItem>();
            listaMenus.Add(new SelectListItem { Text = "Mantenimiento", Value = "1" });
            listaMenus.Add(new SelectListItem { Text = "Consultas", Value = "2" });
            listaMenus.Add(new SelectListItem { Text = "Accesibilidad", Value = "3" });
            listaMenus.Add(new SelectListItem { Text = "Citas", Value = "4" });
            ViewBag.ListaMenu = listaMenus;
        }

        private void CargarControlador()
        {
            List<SelectListItem> listaControlador = new List<SelectListItem>();
            listaControlador.Add(new SelectListItem { Text = "AsignaRol", Value = "1" });
            listaControlador.Add(new SelectListItem { Text = "Citas", Value = "2" });
            listaControlador.Add(new SelectListItem { Text = "Enfermedad", Value = "3" });
            listaControlador.Add(new SelectListItem { Text = "Especilidad", Value = "4" });
            listaControlador.Add(new SelectListItem { Text = "Medico", Value = "5" });
            listaControlador.Add(new SelectListItem { Text = "Paciente", Value = "6" });
            listaControlador.Add(new SelectListItem { Text = "Pagina", Value = "7" });
            listaControlador.Add(new SelectListItem { Text = "TipoUsuarios", Value = "8" });
            listaControlador.Add(new SelectListItem { Text = "Usuario", Value = "9" });
            listaControlador.Add(new SelectListItem { Text = "ConsultaCitas", Value = "10" });
            listaControlador.Add(new SelectListItem { Text = "ConsultaEnfermedad", Value = "11" });
            listaControlador.Add(new SelectListItem { Text = "ConsultaEspecialidad", Value = "12" });
            listaControlador.Add(new SelectListItem { Text = "ConsultaMedico", Value = "13" });
            listaControlador.Add(new SelectListItem { Text = "ConsultaPacientes", Value = "14" });
            listaControlador.Add(new SelectListItem { Text = "ConsultaUsuario", Value = "15" });
            ViewBag.ListaControlador = listaControlador;
        }
        /*CRUD*/
        public void CargarUltimoRegistro()
        {
            listaPagina = (from pagina in _db.Pagina
                           select new Pagina
                           {
                               PaginaId = pagina.PaginaId,
                               Menu = pagina.Menu,
                               Controlador = pagina.Controlador
                           }).ToList();
            var ultimoRegistro = listaPagina.OrderByDescending(e => e.PaginaId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;
            }
            else
            {
                ViewBag.ID = ultimoRegistro.PaginaId + 1;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {

            CargarUltimoRegistro();
            CargarMenus();
            CargarControlador();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pagina pagina)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(pagina);
                }
                else
                {

                    CargarUltimoRegistro();
                    Pagina _pagina = new Pagina()
                    {
                        PaginaId = ViewBag.ID,
                        Menu = pagina.Menu,
                        BotonHabilitado = 1,
                        Accion = pagina.Accion,
                        Controlador = pagina.Controlador,
                    };

                    _db.Pagina.Add(_pagina);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction("Index", "TipoUsuarios");
        }

        public IActionResult Details(int id)
        {
            CargarMenus();
            CargarUltimoRegistro();
            int recCount = _db.Pagina.Count(e => e.PaginaId == id);
            Pagina _pagina = (from p in _db.Pagina
                              where p.PaginaId == id
                              select p).DefaultIfEmpty().Single();
            return View(_pagina);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CargarMenus();
            CargarUltimoRegistro();
            int recCount = _db.Pagina.Count(e => e.PaginaId == id);
            Pagina _pagina = (from p in _db.Pagina
                              where p.PaginaId == id
                              select p).DefaultIfEmpty().Single();
            return View(_pagina);
        }

        [HttpPost]
        public IActionResult Edit(Pagina pagina)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(pagina);
                }
                else
                {
                    rpta = "OK";

                    Pagina _pagina = new Pagina()
                    {
                        PaginaId = pagina.PaginaId,
                        Menu = pagina.Menu,
                        BotonHabilitado = 1,
                        Accion = pagina.Accion,
                        Controlador = pagina.Controlador,
                    };
                    _db.Pagina.Update(_pagina);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return RedirectToAction("Index", "TipoUsuarios");
        }

        [HttpPost]
        public IActionResult Delete(int PaginaId)
        {
            var Error = "";
            try
            {
                Pagina _pagina = _db.Pagina
                             .Where(e => e.PaginaId == PaginaId).First();
                _db.Pagina.Remove(_pagina);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction("Index", "TipoUsuarios");
        }

        public void CargarUsuario()
        {
            ViewBag.Usuario = HttpContext.Session.GetString("nombreUsuario");
        }
    }
}
