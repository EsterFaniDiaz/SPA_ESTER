using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}