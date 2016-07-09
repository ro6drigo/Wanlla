using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wanlla.Filters;
using Modelo;

namespace Wanlla.Controllers
{
    [Autenticado]
    public class PerfilController : Controller
    {
        private usuario usuario = new usuario();

        public ActionResult CambiarPassword(int id = 0)
        {
            ViewBag.id = id;
            return View(usuario.obtener(SessionHelper.Leer<int>("id_usuario")));
        }

        public ActionResult GuardarPassword(usuario model, string pass_actual, string new_pass, string conf_pass)
        {
            if (model.validarPasswordActual(pass_actual, SessionHelper.Leer<int>("id_usuario")))
            {
                if (model.confirmarPassword(new_pass, conf_pass))
                {
                    model.cambiarPassword(new_pass, SessionHelper.Leer<int>("id_usuario"));
                    return Redirect("~/Home/");
                }
                else
                {
                    return Redirect("~/Perfil/CambiarPassword/2");
                }
            }
            else
            {
                return Redirect("~/Perfil/CambiarPassword/1");
            }
        }
    }
}
