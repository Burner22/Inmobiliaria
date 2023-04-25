using Inmobiliaria2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
            ViewBag.Error = TempData["Error"];
            return View();
        }
        [Authorize]
        public ActionResult Seguro()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            return View(claims);
        }

        [Authorize(Policy = "Administrador")]
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Restringido()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Ver()
        {   
            return View();
        }

        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }

        public IActionResult Error(int codigo)
		{
			if(codigo == 404)
            {
                return View();
            }
            else
            {
                return View();
            }
			
		}

    }
}