using ClassLibrary1;
using ClassLibrary1.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SPA_ESTER.Controllers
{
    public class LoginController : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuariosModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = db.Usuarios
                    .FirstOrDefault(u => u.usuario == model.usuario && u.contraseña == model.contraseña);

                if (usuario != null)
                {
                    // Aquí puedes implementar la lógica para establecer la sesión del usuario
                    FormsAuthentication.SetAuthCookie(usuario.usuario, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
