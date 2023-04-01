using Inmobiliaria2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Inmobiliaria2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult Ver()
        //{
        //    PropietarioRepositorio propietarioRepositorio = new PropietarioRepositorio();
        //    var lista = propietarioRepositorio.GetPropietarios();
        //    return View(lista);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}