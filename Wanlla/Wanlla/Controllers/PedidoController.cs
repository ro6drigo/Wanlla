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
                ViewBag.dieta = pedido.obtenerDieta(id, SessionHelper.Leer<int>("id_usuario"));
                return View(new pedido());
            }
            else
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
        }

        public ActionResult CrearPedido(string dieta)
        {
            pedido n_pedido = new pedido();
            n_pedido.id_usuario = SessionHelper.Leer<int>("id_usuario");
            n_pedido.fec_pedido = DateTime.Now;
            n_pedido.est_pedido = "En Espera";

            n_pedido.crearPedido();

            return Redirect("~/Pedido/Producto/" + n_pedido.id_pedido + "?die=" + dieta + "&ing=0");
        }

        public ActionResult Producto(int id, string die, string ing, pedido_producto model)
        {
            if (model.id_producto != null)
            {
                if (ModelState.IsValid)
                {
                    model.guardar();
                }
                else
                {
                    ViewBag.id = id;
                    ViewBag.die = die;
                    ViewBag.ing = (Convert.ToInt16(ing) - 1);
                    return View("~/Views/Pedido/Producto.cshtml", model);
                }
            }

            if (pedido.validarPedido(id))
            {
                if (pedido.validarDieta(Convert.ToInt16(die)))
                {
                    var ingredientes = pedido.obtenerTotalIng(Convert.ToInt16(die));
                    var ped_pro = new pedido_producto();

                    if (Convert.ToInt16(ing) <= ingredientes.GetUpperBound(0))
                    {
                        ViewBag.id = id;
                        ViewBag.die = die;
                        ViewBag.ing = ing;
                        return View(new pedido_producto());
                    }
                    else
                    {
                        return Redirect("~/Pedido/Confirmar/" + id);
                    }
                }
                else
                {
                    return Redirect("~/Home");
                }
            }
            else
            {
                return Redirect("~/Home");
            }
        }
    }
}
