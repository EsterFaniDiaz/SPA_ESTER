using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;

namespace SPA_CLIENTE.Controllers
{
    public class ServiciosController : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        // GET: Servicios
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                // Obtiene el nombre de usuario
                string username = User.Identity.Name;

                // Puedes hacer algo con el nombre de usuario, como buscar información adicional en la base de datos
                ViewBag.Username = username;
            }
            else
            {
                ViewBag.Username = "Invitado";
            }


            return View(db.Servicios.ToList());
        }



        public ActionResult GetImage(int id)
        {
            var servicio = db.Servicios.Find(id);
            if (servicio != null && servicio.imagen_servicio != null)
            {
                return File(servicio.imagen_servicio, "image/jpeg"); // Ajusta el tipo de contenido según tu imagen
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: Servicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicios servicios = db.Servicios.Find(id);
            if (servicios == null)
            {
                return HttpNotFound();
            }
            return View(servicios);
        }

        // GET: Servicios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_servicios,precio_ser,descripción_ser,nombre_ser")] Servicios servicios)
        {
            if (ModelState.IsValid)
            {
                db.Servicios.Add(servicios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servicios);
        }

        // GET: Servicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicios servicios = db.Servicios.Find(id);
            if (servicios == null)
            {
                return HttpNotFound();
            }
            return View(servicios);
        }

        // POST: Servicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_servicios,precio_ser,descripción_ser,nombre_ser")] Servicios servicios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servicios);
        }

        // GET: Servicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicios servicios = db.Servicios.Find(id);
            if (servicios == null)
            {
                return HttpNotFound();
            }
            return View(servicios);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servicios servicios = db.Servicios.Find(id);
            db.Servicios.Remove(servicios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
