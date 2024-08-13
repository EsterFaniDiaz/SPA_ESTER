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
using ClassLibrary1.ViewModels;

namespace SPA_CLIENTE.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        // GET: Reservas

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


        public ActionResult ListarReservas()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Obtiene el nombre de usuario
                string username1 = User.Identity.Name;

                // Puedes hacer algo con el nombre de usuario, como buscar información adicional en la base de datos
                ViewBag.Username = username1;
            }
            else
            {
                ViewBag.Username = "Invitado";
            }

            var reservas = db.Reservas.Include(r => r.Clientes).Include(r => r.Empleados).Include(r => r.Metodos_Pago).Include(r => r.Reservas_servicios);

            // Obtiene el nombre de usuario
            string username = User.Identity.Name;

            var usuario = db.Usuarios.FirstOrDefault(u => u.usuario == username);
            var cliente = usuario != null ? db.Clientes.FirstOrDefault(u => u.id_usuario == usuario.id_usuario) : null;

            // Mapeo manual de Reservas a ListarReservasModel
            var listarReservas = reservas.Select(r => new ListarReservasModel
            {
                id_reservas = r.id_reservas,
                estado_reserva = r.estado_reserva,
                id_empleados = r.id_empleados,
                id_clientes = r.id_clientes,
                id_metodos_pg = r.id_metodos_pg,
                fecha_reserva = r.fecha_reserva,
                Clientes = r.Clientes,
                Empleados = r.Empleados,
                Metodos_Pago = r.Metodos_Pago,
                Servicio = r.Reservas_servicios.FirstOrDefault().Servicios,
                Facturas = r.Facturas,
                Reservas_servicios = r.Reservas_servicios,
                id_factura = r.Facturas.FirstOrDefault().id_factura,
            }).Where(re => re.estado_reserva != null && re.estado_reserva != "CANCELADA" && re.id_clientes == cliente.id_clientes).ToList();

            return View(listarReservas.ToList());
        }

        // GET: Reservas/CreateReseva
        public ActionResult CreateReserva(int? id_servicios)
        {
            if (id_servicios == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Servicios Servicio = db.Servicios.Find(id_servicios);

            ReservasViewModel reservasViewModel = new ReservasViewModel();

            reservasViewModel.Servicios = new List<Servicios> { Servicio };
            reservasViewModel.id_servicios = id_servicios;


            ViewBag.id_clientes = new SelectList(db.Clientes, "id_clientes", "nombre_cl");
            ViewBag.id_empleados = new SelectList(db.Empleados, "id_empleados", "nombre_emp");
            ViewBag.id_metodos_pg = new SelectList(db.Metodos_Pago, "id_metodos_pg", "nombre_metodo");

            return View(reservasViewModel);
        }

        // POST: Reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_metodos_pg,FechaHora,id_servicios")] ReservasViewModel reservasModel)
        {
            if (ModelState.IsValid)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Obtiene el nombre de usuario
                string username = User.Identity.Name;

                var usuario = db.Usuarios.Where(u => u.usuario == username).FirstOrDefault();
                var cliente = db.Clientes.Where(c => c.id_usuario == usuario.id_usuario).FirstOrDefault();

                reservasModel.id_clientes = cliente.id_clientes;

                Reservas reservas = new Reservas
                {
                    id_clientes = cliente.id_clientes,
                    id_metodos_pg = reservasModel.id_metodos_pg,
                    fecha_reserva = reservasModel.FechaHora,
                    estado_reserva = "DESASIGNADA"
                };

                db.Reservas.Add(reservas);
                db.SaveChanges();

                Reservas_servicios reservas_Servicios = new Reservas_servicios
                {
                    id_reservas = reservas.id_reservas,
                    id_servicios = reservasModel.id_servicios
                };

                db.Reservas_servicios.Add(reservas_Servicios);

                db.SaveChanges();

                return RedirectToAction("ListarReservas");
            }

            ViewBag.id_clientes = new SelectList(db.Clientes, "id_clientes", "nombre_cl", reservasModel.id_clientes);
            ViewBag.id_empleados = new SelectList(db.Empleados, "id_empleados", "nombre_emp", reservasModel.id_empleados);
            ViewBag.id_metodos_pg = new SelectList(db.Metodos_Pago, "id_metodos_pg", "id_metodos_pg", reservasModel.id_metodos_pg);
            return View(reservasModel);
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
            if (reservas == null)
            {
                return HttpNotFound();
            }

            reservas.estado_reserva = "CANCELADA";
            db.Entry(reservas).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListarReservas");
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
