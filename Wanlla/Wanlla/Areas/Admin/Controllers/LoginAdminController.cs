using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Wanlla.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        private usuario usuario = new usuario();
        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Acceder(string email_usuario, string pass_usuario)
        {
            var rm = usuario.accederAdmin(email_usuario, pass_usuario);

            if (rm.response)
            {
                return Redirect("~/Admin/Admin");
            }
            else
            {
                return Redirect("~/Admin/LoginAdmin/Index/" + rm.message);
            }
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            SessionHelper.DestruirSesion();
            return Redirect("~/Admin/LoginAdmin");
        }
    }
}