using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Wanlla.Filters;

namespace Wanlla.Controllers
{
    [Autenticado]
    public class PedidoController : Controller
    {
        private pedido pedido = new pedido();
        private dieta dieta = new dieta();

        // GET: Pedido
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dieta(int id)
        {
            if(pedido.obtenerDieta(id, SessionHelper.Leer<int>("id_usuario")) != null)
            {
                ViewBag.TotalIng = pedido.obtenerTotalIng(id);
                return View(pedido.obtenerDieta(id, SessionHelper.Leer<int>("id_usuario")));
            }
            else
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
        }
    }
}
