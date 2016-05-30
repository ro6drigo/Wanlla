using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wanlla.Controllers
{
    public class DietaController : Controller
    {
        // GET: Dieta
        public ActionResult Index()
        {
            return View();
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