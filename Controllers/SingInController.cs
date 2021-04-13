using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class SingInController : Controller
    {
        public IActionResult Index()
        {
            cargarTipoUsuarios();
            return View();
        }

        private readonly DBClinicaAcmeContext _db;

        public SingInController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        public string _SingIn(int UserId, int TipoUsuario, string NombreUsuario, string Password)
        {
            string rpta = "";
            string claveCifrada = Utilitarios.CifrarDatos(Password);
            try
            {
                int nVeces = _db.Usuario.Where(u => u.UsuarioId == UserId).Count();
                if (nVeces != 0)
                {
                    rpta = "Ya existe el usuario";
                    return rpta;
                }
                else
                {
                    Usuario _usuario = new Usuario();
                    _usuario.UsuarioId = UserId;
                    _usuario.TipoUsuarioId = TipoUsuario;
                    _usuario.Nombre = NombreUsuario;
                    _usuario.Password = claveCifrada;
                    _db.Usuario.Add(_usuario);
                    _db.SaveChanges();
                    rpta = "OK";
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        private void cargarTipoUsuarios()
        {
            List<SelectListItem> listaTipoUsuario = new List<SelectListItem>();
            listaTipoUsuario = (from tipoUsuario in _db.TipoUsuario
                                orderby tipoUsuario.Nombre
                                select new SelectListItem
                                {
                                    Text = tipoUsuario.Nombre,
                                    Value = tipoUsuario.TipoUsuarioId.ToString()
                                }).ToList();
            ViewBag.ListaTipoUsuario = listaTipoUsuario;
        }
    }
}
