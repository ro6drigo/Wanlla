using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Wanlla.Areas.Admin.Controllers
{
    public class ReportesController : Controller
    {
        private favorito favorito = new favorito();
        private receta receta = new receta();
        // GET: Admin/Reportes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FavoritosPie()
        {
            ViewBag.Favorito = favorito.RMasFavorito();
            return View(receta.Listar());
        }
        public ActionResult FavoritosDonut()
        {
            ViewBag.Favorito = favorito.RMasFavorito();
            return View(receta.Listar());
        }
        public ActionResult FavoritosBar()
        {
            ViewBag.Favorito = favorito.RMasFavorito();
            return View(receta.Listar());
        }

        public ActionResult RecetasPie()
        {
            return View(receta.Consulta());
        }
        public ActionResult RecetasDonut()
        {
            return View(receta.Consulta());
        }
        public ActionResult RecetasBar()
        {
            return View(receta.Consulta());
        }
    }
}