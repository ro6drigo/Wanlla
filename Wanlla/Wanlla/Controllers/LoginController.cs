using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

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
            var rm = usuario.acceder(email_usuario, pass_usuario);

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
            SessionHelper.DestruirSesion();
            return Redirect("~/");
        }

        public ActionResult Guardar(usuario model)
        {
            ModelState.Remove("tipo_usuario");
            model.tipo_usuario = "Usuario";
            if (ModelState.IsValid)
            {
                if (model.validarCorreo())
                {
                    model.registrar();
                    return Redirect("~/Home"); // Devuelve el index
                }
                else
                {
                    ModelState.AddModelError("email_usuario", "Ya existe un usuario con este correo electrónico");
                    return View("~/Views/Login/Registro.cshtml", model);
                }
            }
            else
            {
                return View("~/Views/Login/Registro.cshtml", model);
            }
        }
    }
}