using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Wanlla.Controllers
{
    public class PedidoController : Controller
    {
        private pedido pedido = new pedido();

        // GET: Pedido
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dieta(int id)
        {
            if(pedido.obtenerDieta(id, SessionHelper.Leer<int>("id_usuario")) != null)
            {
                return View(pedido.obtenerDieta(id, SessionHelper.Leer<int>("id_usuario")));
            }
            else
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
        }
    }
}
