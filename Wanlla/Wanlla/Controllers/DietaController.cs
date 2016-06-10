using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wanlla.Filters;

namespace Wanlla.Controllers
{
    [Autenticado]
    public class DietaController : Controller
    {
        // GET: Dieta
        public ActionResult Index()
        {
            return View();
            Session["hola"] = "";
        }

        public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Mantenimiento()
        {
            return View();
        }
    }
}