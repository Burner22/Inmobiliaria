using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using Inmobiliaria2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;
using NuGet.ContentModel;

namespace inmobiliaria2.Controllers
{
    public class PropietarioController : Controller
    {
        private readonly PropietarioRepositorio repo;

        public PropietarioController()
        {
            repo = new PropietarioRepositorio();
        }


        // GET: Propietario
        public ActionResult Index()
        {

            var lista = repo.GetPropietarios();
            return View(lista);
        }

        // GET: Propietario/Details/5
        public ActionResult Details(int id)
        {
            var propietario = repo.GetPropietario(id);
            return View(propietario);
        }

        // GET: Propietario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario propietario)
        {
            try
            {
                // TODO: Add insert logic here
                var repo = new PropietarioRepositorio();
                repo.Alta(propietario);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception exp)
            {
                throw;
            }
        }

        // GET: Propietario/Edit/5
        public ActionResult Edit(int id)
        {
            var propietario = repo.GetPropietario(id);
            return View(propietario);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Propietario propietario)
        {
            try
            {
                // TODO: Add update logic here
                
                repo.Modificar(propietario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                return View(exc);
            }
        }

        // GET: Propietario/Delete/5
        public ActionResult Delete(int id)
        {
            var propietario = repo.GetPropietario(id);
            return View(propietario);
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                repo.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}

