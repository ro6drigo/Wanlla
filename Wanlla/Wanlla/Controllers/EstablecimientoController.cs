using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Wanlla.Controllers
{
    public class EstablecimientoController : Controller
    {
        private establecimiento establecimiento = new establecimiento();
        private receta_establecimiento rec_estable = new receta_establecimiento();

        // GET: Establecimiento
        public ActionResult Index()
        {
            return View(establecimiento.Listar());
        }

        public ActionResult CrearMapa()
        {
            //return View(rec_estable.Listar());
            return View();
        }
    }
}