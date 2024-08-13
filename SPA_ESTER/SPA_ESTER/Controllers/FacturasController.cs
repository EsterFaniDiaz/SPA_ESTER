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
    public class FacturasController : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        // GET: Facturas
        public ActionResult Index()
        {
            var facturas = db.Facturas.Include(f => f.Reservas);
            return View(facturas.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // GET: Facturas/Details/5
        public ActionResult Factura(int? id_factura)
        {
            if (id_factura == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var factura = db.Facturas.Find(id_factura);

            if (factura == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reservas reserva = db.Reservas.Find(factura.id_reservas);

            FacturasModel facturaModel = new FacturasModel
            {

                id_factura = factura.id_factura,
                id_reservas = reserva.id_reservas,
                id_empleados = reserva.id_empleados,
                id_clientes = reserva.id_clientes,
                id_metodos_pg = reserva.id_metodos_pg,
                fecha_reserva = reserva.fecha_reserva,
                Clientes = reserva.Clientes,
                Empleados = reserva.Empleados,
                Metodos_Pago = reserva.Metodos_Pago,
                Servicio = reserva.Reservas_servicios.FirstOrDefault().Servicios
            };

            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(facturaModel);
        }

        // GET: Facturas/Details/5
        public ActionResult PreFactura(int? idReserva)
        {
            if (idReserva == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservas reserva = db.Reservas.Find(idReserva);

            FacturasModel Prefactura = new FacturasModel
            {

                id_reservas = reserva.id_reservas,
                id_empleados = reserva.id_empleados,
                id_clientes = reserva.id_clientes,
                id_metodos_pg = reserva.id_metodos_pg,
                fecha_reserva = reserva.fecha_reserva,
                Clientes = reserva.Clientes,
                Empleados = reserva.Empleados,
                Metodos_Pago = reserva.Metodos_Pago,
                Servicio = reserva.Reservas_servicios.FirstOrDefault().Servicios,
                id_servicios = reserva.Reservas_servicios.FirstOrDefault().Servicios.id_servicios
            };

            if (Prefactura == null)
            {
                return HttpNotFound();
            }
            return View(Prefactura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarFactura([Bind(Include = "id_servicios,id_reservas")] FacturasModel facturas)
        {
            if (ModelState.IsValid)
            {
                if (facturas.id_reservas == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservas reserva = db.Reservas.Find(facturas.id_reservas);

                reserva.estado_reserva = "FACTURADA";

                db.Entry(reserva).State = EntityState.Modified;

                Facturas factura = new Facturas
                {
                    id_reservas = reserva.id_reservas
                };

                db.Facturas.Add(factura);
                db.SaveChanges();

                Facturas_servicios facturas_Servicios = new Facturas_servicios
                {
                    id_factura = factura.id_factura,
                    id_servicios = facturas.id_servicios
                };

                db.Facturas_servicios.Add(facturas_Servicios);

                db.SaveChanges();

                return RedirectToAction("Factura", new { id_factura = factura.id_factura });
            }

            ViewBag.id_reservas = new SelectList(db.Reservas, "id_reservas", "id_reservas", facturas.id_reservas);
            return View(facturas);

            
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.id_reservas = new SelectList(db.Reservas, "id_reservas", "id_reservas");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_factura,id_reservas")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Facturas.Add(facturas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_reservas = new SelectList(db.Reservas, "id_reservas", "id_reservas", facturas.id_reservas);
            return View(facturas);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_reservas = new SelectList(db.Reservas, "id_reservas", "id_reservas", facturas.id_reservas);
            return View(facturas);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_factura,id_reservas")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_reservas = new SelectList(db.Reservas, "id_reservas", "id_reservas", facturas.id_reservas);
            return View(facturas);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturas facturas = db.Facturas.Find(id);
            db.Facturas.Remove(facturas);
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
