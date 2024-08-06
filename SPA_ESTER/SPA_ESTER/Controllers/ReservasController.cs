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
    public class ReservasController : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        // GET: Reservas
        public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r.Clientes).Include(r => r.Empleados).Include(r => r.Metodos_Pago);
            return View(reservas.ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            return View(reservas);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.id_clientes = new SelectList(db.Clientes, "id_clientes", "nombre_cl");
            ViewBag.id_empleados = new SelectList(db.Empleados, "id_empleados", "nombre_emp");
            ViewBag.id_metodos_pg = new SelectList(db.Metodos_Pago, "id_metodos_pg", "id_metodos_pg");
            return View();
        }

        // POST: Reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_reservas,id_empleados,id_clientes,id_metodos_pg")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Reservas.Add(reservas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_clientes = new SelectList(db.Clientes, "id_clientes", "nombre_cl", reservas.id_clientes);
            ViewBag.id_empleados = new SelectList(db.Empleados, "id_empleados", "nombre_emp", reservas.id_empleados);
            ViewBag.id_metodos_pg = new SelectList(db.Metodos_Pago, "id_metodos_pg", "id_metodos_pg", reservas.id_metodos_pg);
            return View(reservas);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_clientes = new SelectList(db.Clientes, "id_clientes", "nombre_cl", reservas.id_clientes);
            ViewBag.id_empleados = new SelectList(db.Empleados, "id_empleados", "nombre_emp", reservas.id_empleados);
            ViewBag.id_metodos_pg = new SelectList(db.Metodos_Pago, "id_metodos_pg", "id_metodos_pg", reservas.id_metodos_pg);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_reservas,id_empleados,id_clientes,id_metodos_pg")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_clientes = new SelectList(db.Clientes, "id_clientes", "nombre_cl", reservas.id_clientes);
            ViewBag.id_empleados = new SelectList(db.Empleados, "id_empleados", "nombre_emp", reservas.id_empleados);
            ViewBag.id_metodos_pg = new SelectList(db.Metodos_Pago, "id_metodos_pg", "id_metodos_pg", reservas.id_metodos_pg);
            return View(reservas);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reservas = db.Reservas.Find(id);
            if (reservas == null)
            {
                return HttpNotFound();
            }
            return View(reservas);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservas reservas = db.Reservas.Find(id);
            db.Reservas.Remove(reservas);
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
