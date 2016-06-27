using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Wanlla.Controllers
{
    public class HomeController : Controller
    {
        private receta receta = new receta();

        // GET: Home
        public ActionResult Index(int id = 1)
        {
            ViewBag.count = id;
            ViewBag.cant = receta.cantPaginador();
            return View(receta.listar(id));
        }
    }
}