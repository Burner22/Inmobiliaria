using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Inmobiliaria2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Inmobiliaria2.Controllers
{
    
    public class InmuebleController : Controller
    {
        public string UrlBase = "http://173.249.49.169:88";
        PropietarioRepositorio propietario = new PropietarioRepositorio();
        // GET: Inmueble
        public ActionResult Index()
        {
            InmuebleRepositorio inmuRepo = new InmuebleRepositorio();
            var lista = inmuRepo.GetInmuebles();
            return View(lista);
        }

        // GET: Inmueble/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inmueble/Create
        public ActionResult Create()
        {
            try
            {
            ViewBag.Propietario = propietario.GetPropietarios();
            return View();
            }
            catch(Exception exc)
            {
                throw;
            }
            
        }

        // POST: Inmueble/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                InmuebleRepositorio inmuRepo = new InmuebleRepositorio();
                inmuRepo.Alta(inmueble);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exp)
            {
                throw;
            }
        }

        // GET: Inmueble/Edit/5
        public ActionResult Edit(int id)
        {
            InmuebleRepositorio inmuRepo = new InmuebleRepositorio();
            var inmueble = inmuRepo.GetInmueble(id);
            return View(inmueble);
        }

        // POST: Inmueble/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmueble/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inmueble/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //[HttpPost]
        //public async Task<ActionResult> Ubicacion(int id, string latitud, string longitud)
        //{
        //    Inmueble inmu = new Inmueble();
        //    using (var client = new HttpClient())
        //    {
        //        var noinmueble = Convert.ToInt64(id);
        //        client.BaseAddress = new Uri(UrlBase);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = await client.GetAsync("api/test/consulta/" +noinmueble);
                
        //        if(response.IsSuccessStatusCode)
        //        {
        //            string restResponse = response.Content.ReadAsStringAsync().Result;
        //            inmu = JsonConvert.DeserializeObject<Inmueble>(restResponse);
        //            inmu.Longitud = longitud;
        //            inmu.Latitud = latitud;

        //        }
        //        else
        //        {
        //            return RedirectToAction("Inexistente", "pagina");
        //        }

        //    }
        //    return View(inmu);
        //}

    }
}