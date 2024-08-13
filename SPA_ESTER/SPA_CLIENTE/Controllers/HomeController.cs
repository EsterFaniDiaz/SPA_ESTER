using ClassLibrary1;
using ClassLibrary1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SPA_CLIENTE.Controllers
{
    public class HomeController : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        public ActionResult Index()
        {
            return View(db.Servicios.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        // POST: Reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_clientes,id_metodos_pg,FechaHora,id_servicios")] ReservasViewModel reservas)
        {
            if (ModelState.IsValid)
            {
                Reservas reserva = new Reservas
                {
                    id_clientes = reservas.id_clientes,
                    id_metodos_pg = reservas.id_metodos_pg,
                    fecha_reserva = reservas.FechaHora
                };

                db.Reservas.Add(reserva);
                db.SaveChanges();

                Reservas_servicios reservas_Servicios = new Reservas_servicios
                {
                    id_reservas = reserva.id_reservas,
                    id_servicios = reservas.id_servicios
                };

                db.Reservas_servicios.Add(reservas_Servicios);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_clientes = new SelectList(db.Clientes, "id_clientes", "nombre_cl", reservas.id_clientes);
            ViewBag.id_empleados = new SelectList(db.Empleados, "id_empleados", "nombre_emp", reservas.id_empleados);
            ViewBag.id_metodos_pg = new SelectList(db.Metodos_Pago, "id_metodos_pg", "id_metodos_pg", reservas.id_metodos_pg);
            return View(reservas);
        }



        // GET: Servicios/Details/5
        public ActionResult ServiciosDetails(int? id)
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



        // GET: Reservas/CreateReseva
        public ActionResult Reserva(int? id_servicios)
        {
            if (id_servicios == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Servicios Servicio = db.Servicios.Find(id_servicios);

            ReservasViewModel reservasViewModel = new ReservasViewModel();

            reservasViewModel.Servicios = new List<Servicios> { Servicio };
            reservasViewModel.id_servicios = Servicio.id_servicios;


            ViewBag.id_clientes = new SelectList(db.Clientes, "id_clientes", "nombre_cl");
            ViewBag.id_empleados = new SelectList(db.Empleados, "id_empleados", "nombre_emp");
            ViewBag.id_metodos_pg = new SelectList(db.Metodos_Pago, "id_metodos_pg", "id_metodos_pg");

            return View(reservasViewModel);
        }
    }
}