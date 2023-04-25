using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Core.Types;


namespace Inmobiliaria2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        private readonly UsuarioRepositorio repositorio;

        public UsuarioController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
            repositorio = new UsuarioRepositorio();
        }

		[Authorize(Policy = "Administrador")]
		public IActionResult Index()
        {
            IList<Usuario> usuarios = repositorio.ObtenerTodos();
            return View(usuarios);
        }

		[Authorize(Policy = "Administrador")]
		public ActionResult Details(int id)
		{
			var e = repositorio.ObtenerPorId(id);
			return View(e);
		}

		[Authorize]
		public IActionResult Edit(string miClaim)
        {
            ViewData["Title"] = "Editar usuario";
            var usuario = repositorio.ObtenerPorEmail(miClaim);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View(usuario);
        }

		[Authorize]
		public IActionResult EditId(int id)
		{
			ViewData["Title"] = "Editar usuario";
			var usuario = repositorio.ObtenerPorId(id);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View("Edit",usuario);
        }

       
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<ActionResult> Edit(int id, Usuario u)
    {
        try
        {
            var usser = repositorio.ObtenerPorId(id);

            if (User.Identity.Name == usser.Email) //Mail traido de la bd es igual al claim actual, cambia sus claims por las actuales
            {
                var identity = (ClaimsIdentity)User.Identity;
                identity.RemoveClaim(identity.FindFirst("FullName"));
                identity.AddClaim(new Claim("FullName", u.Nombre + " " + u.Apellido));

                identity.RemoveClaim(identity.FindFirst(ClaimTypes.Name));  //Actualizo las claims para que cuando el usuario edita su perfil
                identity.AddClaim(new Claim(ClaimTypes.Name, u.Email));     //Luego pueda entrar

                identity.RemoveClaim(identity.FindFirst(ClaimTypes.Role));
                identity.AddClaim(new Claim(ClaimTypes.Role, usser.RolNombre));

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }

            usser.Nombre = u.Nombre;
            usser.Apellido = u.Apellido;
            usser.Email = u.Email;
            usser.Rol = (User.IsInRole("Administrador") || User.IsInRole("SuperAdministrador")) ? u.Rol : (int)enRoles.Empleado;

            if (u.AvatarFile != null && u.Id > 0)
            {

                string wwwPath = environment.WebRootPath;

                if (usser.Avatar != @"/imgPerfil\avatar.png")
                {
                    string urlAvatar = usser.Avatar.Substring(1);             //no funciona sin el SubString(1)
                    System.IO.File.Delete(Path.Combine(wwwPath, urlAvatar)); // elimino la imagen que tenia antes

                }

                string path = Path.Combine(wwwPath, "imgPerfil");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Path.GetFileName(u.AvatarFile.FileName);//este nombre se puede repetir
                string fileName = "avatar_" + u.Id + Path.GetExtension(u.AvatarFile.FileName);
                string pathCompleto = Path.Combine(path, fileName);
                u.Avatar = Path.Combine("/imgPerfil", fileName);

                // Esta operación guarda la foto en memoria en el ruta que necesitamos
                using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                {
                    u.AvatarFile.CopyTo(stream);
                }

            }
            else
            {
                u.Avatar = usser.Avatar;
            }

            

            repositorio.EditarPerfil(u);
            TempData["Mensaje"] = "Usuario Editado con exito";
            return RedirectToAction("Index", "Home");
			}
        catch (Exception ex)
        {//colocar breakpoints en la siguiente línea por si algo falla
            throw;
        }
    }

    [Authorize(Policy = "Administrador")]
		public ActionResult Create()
		{
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "Administrador")]
		public ActionResult Create(Usuario u)
		{
			if (!ModelState.IsValid)
				return View();
			try
			{
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: u.Clave,
								salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));
				u.Clave = hashed;
                if (User.IsInRole("SuperAdministrador"))
                {
                    u.Rol = u.Rol;
                }
                else
                {
                    u.Rol = (int)enRoles.Empleado;
                    
                }
				
				var nbreRnd = Guid.NewGuid();//posible nombre aleatorio
				int res = repositorio.Alta(u);
				if (u.AvatarFile != null && u.Id > 0)
				{
					string wwwPath = environment.WebRootPath;
					string path = Path.Combine(wwwPath, "imgPerfil");
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					//Path.GetFileName(u.AvatarFile.FileName);//este nombre se puede repetir
					string fileName = "avatar_" + u.Id + Path.GetExtension(u.AvatarFile.FileName);
					string pathCompleto = Path.Combine(path, fileName);
					u.Avatar = Path.Combine("/imgPerfil", fileName);
					// Esta operación guarda la foto en memoria en la ruta que necesitamos
					using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
					{
						u.AvatarFile.CopyTo(stream);
					}
					repositorio.Modificacion(u);
				}
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ViewBag.Roles = Usuario.ObtenerRoles();
				return View();
			}
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ModificarClave(string contraActual, string contraNueva, string contraNuevaRepe)
        {
            try
            {
                var usser = repositorio.ObtenerPorEmail(User.Identity.Name);

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: contraActual,
                        salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));

                contraActual = hashed;

                if (usser.Clave == contraActual)
                {
                    if (contraNueva == contraNuevaRepe)
                    {
                        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: contraNueva,
                                salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
                                prf: KeyDerivationPrf.HMACSHA1,
                                iterationCount: 1000,
                                numBytesRequested: 256 / 8));

                        usser.Clave = hash;
                        repositorio.ModificarClave(usser);

                        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    }
                    else
                    {
						TempData["Mensaje"] = "Las contraseñas nuevas no coinciden";
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    TempData["Mensaje"] = "La contraseña actual no coincide";
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        [AllowAnonymous]   
        public IActionResult Login(string returnUrl)
        {
			if (!User.Identity.IsAuthenticated)
			{
				TempData["returnUrl"] = returnUrl;
                return View();
			}
			else
			{
				return View();
			}
		}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView login)
        {
            try
            {
                var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home" : TempData["returnUrl"].ToString();
                if (ModelState.IsValid)
                {
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: login.Clave,
                        salt: System.Text.Encoding.ASCII.GetBytes("Salt"),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));

                    var e = repositorio.ObtenerPorEmail(login.Usuario);
                    if (e == null || e.Clave != hashed)
                    {
                        ModelState.AddModelError("", "El email o la clave no son correctos");
                        TempData["returnUrl"] = returnUrl;
                        return View();
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, e.Email),
                        new Claim("FullName", e.Nombre + " " + e.Apellido),
                        new Claim(ClaimTypes.Role, e.RolNombre),
                        
                    };

                    var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));
                    TempData["Mensaje"] = "Bienvenido a la Inmobiliaria";
                    TempData.Remove("returnUrl");
					return Redirect(returnUrl);
				}
                TempData["returnUrl"] = returnUrl;
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

		[Route("salir", Name = "Logout")]
		public async Task<ActionResult> Logout()
		{
			await HttpContext.SignOutAsync(
					CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Usuario");
		}

        // GET: Usuarios/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            var usser = repositorio.ObtenerPorId(id);
            return View(usser);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Usuario u)
        {
            try
            {
                var usser = repositorio.ObtenerPorId(id);
                string wwwPath = environment.WebRootPath;

                if (usser.Avatar.Length > 1)
                {
                    string urlAvatar = usser.Avatar.Substring(1);             //no funciona sin el SubString(1)
                    System.IO.File.Delete(Path.Combine(wwwPath, urlAvatar));
                    repositorio.Baja(u);
                    TempData["Mensaje"] = "Usuario Eliminado con exito";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    repositorio.Baja(u);
                    TempData["Mensaje"] = "Usuario Eliminado con exito";
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch
            {
                return View();
            }
        }

    }
}