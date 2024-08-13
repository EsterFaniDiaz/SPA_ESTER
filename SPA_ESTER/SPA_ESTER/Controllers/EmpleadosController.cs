using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;
using ClassLibrary1.Models;

namespace SPA_ESTER.Controllers
{

    [Authorize]
    public class EmpleadosController : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        // GET: Empleados
        public ActionResult Index()
        {
            return View(db.Empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_empleados,nombre_emp,teléfono_emp,dirección_emp,correo_emp")] EmpleadosModel empleados)
        {
            if (ModelState.IsValid)
            {
                Empleados empleados1 = new Empleados
                {
                    nombre_emp = empleados.nombre_emp,
                    teléfono_emp = empleados.teléfono_emp,
                    dirección_emp = empleados.dirección_emp,
                    correo_emp = empleados.correo_emp
                };

                db.Empleados.Add(empleados1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);

            EmpleadosModel empleadosModel = new EmpleadosModel
            {
                id_empleados = empleados.id_empleados,
                nombre_emp = empleados.nombre_emp,
                teléfono_emp = empleados.teléfono_emp,
                dirección_emp = empleados.dirección_emp,
                correo_emp = empleados.correo_emp
            }; 

            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleadosModel);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empleados,nombre_emp,teléfono_emp,dirección_emp,correo_emp")] EmpleadosModel empleados)
        {
            if (ModelState.IsValid)
            {
                Empleados empleados1 = db.Empleados.Find(empleados.id_empleados);
                empleados1.nombre_emp = empleados.nombre_emp;
                empleados1.teléfono_emp = empleados.teléfono_emp;
                empleados1.dirección_emp = empleados.dirección_emp;
                empleados1.correo_emp = empleados.correo_emp;


                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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
