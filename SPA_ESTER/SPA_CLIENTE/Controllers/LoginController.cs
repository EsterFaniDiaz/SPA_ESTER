using ClassLibrary1;
using ClassLibrary1.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SPA_CLIENTE.Controllers
{
    public class LoginController : Controller
    {
        private Spa_EsterEntities db = new Spa_EsterEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UsuarioClienteRegistrar()
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



        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar([Bind(Include = "nombre_cl,Numero_Documento,teléfono_cl,dirección_cl,correo_cl,id_usuario")] UsuarioClientesModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var clientes = new Clientes
                {
                    nombre_cl = viewModel.nombre_cl,
                    Numero_Documento = viewModel.Numero_Documento,
                    teléfono_cl = viewModel.teléfono_cl,
                    dirección_cl = viewModel.dirección_cl,
                    correo_cl = viewModel.correo_cl,
                    //id_usuario = viewModel.id_usuario
                };

                db.Clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "usuario", viewModel.id_usuario);
            return View(viewModel);
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
