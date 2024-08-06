using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;

namespace SPA_ESTER.Controllers
{
    public class Metodos_Pago1Controller : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        // GET: Metodos_Pago1
        public ActionResult Index()
        {
            return View(db.Metodos_Pago.ToList());
        }

        // GET: Metodos_Pago1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metodos_Pago metodos_Pago = db.Metodos_Pago.Find(id);
            if (metodos_Pago == null)
            {
                return HttpNotFound();
            }
            return View(metodos_Pago);
        }

        // GET: Metodos_Pago1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Metodos_Pago1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_metodos_pg")] Metodos_Pago metodos_Pago)
        {
            if (ModelState.IsValid)
            {
                db.Metodos_Pago.Add(metodos_Pago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(metodos_Pago);
        }

        // GET: Metodos_Pago1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metodos_Pago metodos_Pago = db.Metodos_Pago.Find(id);
            if (metodos_Pago == null)
            {
                return HttpNotFound();
            }
            return View(metodos_Pago);
        }

        // POST: Metodos_Pago1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_metodos_pg")] Metodos_Pago metodos_Pago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metodos_Pago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metodos_Pago);
        }

        // GET: Metodos_Pago1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metodos_Pago metodos_Pago = db.Metodos_Pago.Find(id);
            if (metodos_Pago == null)
            {
                return HttpNotFound();
            }
            return View(metodos_Pago);
        }

        // POST: Metodos_Pago1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Metodos_Pago metodos_Pago = db.Metodos_Pago.Find(id);
            db.Metodos_Pago.Remove(metodos_Pago);
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
