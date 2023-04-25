using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Inmobiliaria2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace Inmobiliaria2.Controllers
{
    
    public class InmuebleController : Controller
    {
        public string UrlBase = "http://173.249.49.169:88";

        protected readonly IConfiguration configuration;
        private readonly InmuebleRepositorio repositorio;
        private readonly PropietarioRepositorio repoPropietario;
        private readonly ContratoRepositorio repoContrato;
        public InmuebleController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorio = new InmuebleRepositorio();
            repoPropietario = new PropietarioRepositorio();
            repoContrato = new ContratoRepositorio();
        }



		// GET: Inmueble
		[Authorize]
		public ActionResult Index()
        {
            try { 

            var lista = repositorio.GetInmuebles();
            return View(lista);
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }

		[Authorize]
		public ActionResult Disponibles()
        {
            try
            {
                var lista = repositorio.Disponibles();
                return View("Index",lista);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


		[Authorize]
		public ActionResult PorPropietario(int id)
        {

            var lista = repositorio.BuscarPorPropietario(id);
            ViewBag.noindex = 1;
            
            return View("Index", lista);
        }

		// GET: Inmueble/Details/5
		[Authorize]
		public ActionResult Details(int id)
        {

            var inmueble = repositorio.GetInmueble(id);
            ViewBag.Latitud = inmueble.Latitud;
            ViewBag.Longitd = inmueble.Longitud;
            return View(inmueble);
        }

		// GET: Inmueble/Create
		[Authorize]
		public ActionResult Create()
        {
            try
            {
                ViewBag.Propietario = repoPropietario.GetPropietarios();
                return View();
            }
            catch(Exception exc)
            {
                throw;
            }
            
        }

        // POST: Inmueble/Create
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                repositorio.Alta(inmueble);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exp)
            {
                throw;
            }
        }

		// GET: Inmueble/Edit/5
		[Authorize]
		public ActionResult Edit(int id)
        {
            var inmueble = repositorio.GetInmueble(id);
            ViewBag.Propietario = repoPropietario.GetPropietarios();
            ViewBag.Latitud = inmueble.Latitud;
            ViewBag.Longitd = inmueble.Longitud;
            return View(inmueble);
        }

        // POST: Inmueble/Edit/5
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble inmueble)
        {
            try
            {
                // TODO: Add update logic here
                repositorio.Modificar(inmueble);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }

		// GET: Inmueble/Delete/5
		[Authorize]
		public ActionResult Delete(int id)
        {
            var inmueble = repositorio.GetInmueble(id);
            return View(inmueble);
        }

        // POST: Inmueble/Delete/5
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inmueble inmueble)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ContratosDeInmueble(int id)
        {
            try
            {
                InmuebleRepositorio repositorio = new InmuebleRepositorio();
                var lista = repositorio.ContratosDeInmueble(id);
                return View("/Contrato/Index", lista);
            }
            catch
            {
                return View();
            }
        }





    }
}