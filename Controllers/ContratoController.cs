using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria2.Controllers
{
    public class ContratoController : Controller
    {

		// GET: Contrato
		[Authorize]
		public ActionResult Index()
        {
            ContratoRepositorio repositorio = new ContratoRepositorio();
            var lista = repositorio.GetContratos();
            return View(lista);
        }

		// GET: Contrato/Details/5
		[Authorize]
		public ActionResult Details(int id)
        {
            ContratoRepositorio repositorio = new ContratoRepositorio();
            var contrato = repositorio.GetContrato(id);
            return View(contrato);
        }

		// GET: Contrato/Create
		[Authorize]
		public ActionResult Create()
        {
            InquilinoRepositorio inquilino = new InquilinoRepositorio();
            ViewBag.Inquilino = inquilino.GetInquilinos();
            InmuebleRepositorio inmueble = new InmuebleRepositorio();
            ViewBag.Inmueble = inmueble.GetInmueblesDisponibles();
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            try
            {
                InmuebleRepositorio inmueble = new InmuebleRepositorio();
                ContratoRepositorio repositorio = new ContratoRepositorio();
                repositorio.Alta(contrato);
                inmueble.ActualizarInmueble(contrato.IdInmueble);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

		// GET: Contrato/Edit/5
		[Authorize]
		public ActionResult Edit(int id)
        {
            InquilinoRepositorio inquilino = new InquilinoRepositorio();
            ViewBag.Inquilino = inquilino.GetInquilinos();
            InmuebleRepositorio inmueble = new InmuebleRepositorio();
            ViewBag.Inmueble = inmueble.GetInmueblesDisponibles();
            ContratoRepositorio repositorio = new ContratoRepositorio();
            var contrato = repositorio.GetContrato(id);
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato contrato)
        {
            try
            {
                ContratoRepositorio repositorio = new ContratoRepositorio();
                repositorio.Modificar(contrato);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

		// GET: Contrato/Delete/5
		[Authorize]
		public ActionResult Delete(int id)
        {
            ContratoRepositorio repositorio = new ContratoRepositorio();
            var contrato = repositorio.GetContrato(id);
            return View(contrato);
        }

        // POST: Contrato/Delete/5
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato contrato)
        {
            try
            {
				// TODO: Add delete logic here
				ContratoRepositorio repositorio = new ContratoRepositorio();
                repositorio.Baja(contrato);
				return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

		[Authorize]
		public ActionResult DesdeHasta(DateTime desde, DateTime hasta)
        {
            ContratoRepositorio repositorio = new ContratoRepositorio();
            var lista = repositorio.DesdeHasta(desde, hasta);
            return View("Index",lista);
        }

		[Authorize]
		public ActionResult ContratoDesdeHasta(DateTime desde, DateTime hasta)
        {
            ContratoRepositorio repositorio = new ContratoRepositorio();
            var lista = repositorio.ContratoDesdeHasta(desde, hasta);
            return View("/Inmueble/Index", lista);
        }
    }
}