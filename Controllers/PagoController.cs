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
    public class PagoController : Controller
    {
		// GET: Pago
		[Authorize]
		public ActionResult Index()
        {
            PagoRepositorio repositorio = new PagoRepositorio();
            var lista = repositorio.GetPagos();
            return View(lista);
        }

		[Authorize]
		public ActionResult PorContrato(int id)
        {
            PagoRepositorio repositorio = new PagoRepositorio();
            var lista = repositorio.BuscarPorContrato(id);
            ViewBag.IdContrato = id;
            return View("Index",lista);
        }

		[Authorize]
		// GET: Pago/Details/5
		public ActionResult Details(int id)
        {
            PagoRepositorio repositorio = new PagoRepositorio();
            var lista = repositorio.GetPago(id);
            return View(lista);
        }

		[Authorize]
		// GET: Pago/Create
		public ActionResult Create(int id)
        {
            try
            {
                ContratoRepositorio contrato = new ContratoRepositorio();
                ViewBag.Contrato = contrato.GetContrato(id);
                ViewBag.IdContrato = id;
                return View();
            }
            catch(Exception exc)
            {
                throw;
            }
        }

        // POST: Pago/Create
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Create(Pago pago)
        {
            try
            {
                // TODO: Add insert logic here
                PagoRepositorio repositorio = new PagoRepositorio();
                repositorio.Alta(pago);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

		// GET: Pago/Edit/5
		[Authorize]
		public ActionResult Edit(int id)
        {
            PagoRepositorio repositorio = new PagoRepositorio();
            ContratoRepositorio contrato = new ContratoRepositorio();
            ViewBag.Contrato = contrato.GetContratos();
            var pago = repositorio.GetPago(id);
            return View(pago);
        }

        // POST: Pago/Edit/5
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago pago)
        {
            try
            {
                // TODO: Add update logic here
                PagoRepositorio repositorio = new PagoRepositorio();
                repositorio.Modificar(pago);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

		// GET: Pago/Delete/5
		[Authorize]
		public ActionResult Delete(int id)
        {
            PagoRepositorio repositorio = new PagoRepositorio();
            var lista = repositorio.GetPago(id);
            return View(lista);
        }

        // POST: Pago/Delete/5
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago pago)
        {
            try
            {
                // TODO: Add delete logic here
                PagoRepositorio repositorio = new PagoRepositorio();
                repositorio.Baja(pago);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}