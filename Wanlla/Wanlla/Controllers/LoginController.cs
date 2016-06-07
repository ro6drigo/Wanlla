using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Wanlla.Filters;

namespace Wanlla.Controllers
{
    public class LoginController : Controller
    {
        private usuario usuario = new usuario();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult Acceder(string email_usuario, string pass_usuario)
        {
            var rm = usuario.Acceder(email_usuario, pass_usuario);

            if (rm.response)
            {
                return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/Login/Index/" + rm.message);
            }
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }

        public ActionResult Mantenimiento(usuario model)
        {
            if (ModelState.IsValid)
            {
                model.mantenimiento();
                return Redirect("~/Home"); // Devuelve el index
            }
            else
            {
                return View("~/Views/Login/Registro.cshtml", model);
            }
        }
    }
}