using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Wanlla.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
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
                return View("~/Views/Usuario/Registro.cshtml", model);
            }
        }
    }
}