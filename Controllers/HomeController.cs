using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebClinica.Filter;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CargarUsuario();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Rooms()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void CargarUsuario()
        {
            ViewBag.Usuario = HttpContext.Session.GetString("nombreUsuario");
        }
    }
}
