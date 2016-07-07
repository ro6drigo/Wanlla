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
        private favorito favorito = new favorito();

        // GET: Home
        public ActionResult Index(int id = 1, string buscar = "")
        {
            if (SessionHelper.ExisteSesion())
            {
                ViewBag.favoritolist = favorito.Listar(SessionHelper.Leer<int>("id_usuario"));
            }

            if (buscar == "")
            {
                ViewBag.count = id;
                ViewBag.cant = receta.cantPaginador();
                return View(receta.listar(id));
            }
            else
            {
                ViewBag.count = id;
                ViewBag.cant = receta.cantPaginador(buscar);
                return View(receta.listar(id, buscar));
            }
        }

        public ActionResult Receta(int id = 0)
        {
            if (id > 0)
            {
                if (receta.Obtener(id) != null)
                {
                    return View(receta.Obtener(id));
                }
                else
                {
                    return Redirect("~/Home/Index/1");
                }
            }
            else
            {
                return Redirect("~/Home/Index/1");
            }
        }
    }
}