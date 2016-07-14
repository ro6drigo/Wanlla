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
        private ingrediente ingrediente = new ingrediente();
        private pedido pedido = new pedido();
        private dieta dieta = new dieta();
        private pedido_producto pedido_producto = new pedido_producto();

        // GET: Pedido
        public ActionResult Index()
        {
            return View(pedido.listar());
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

        public ActionResult CrearPedido(pedido model)
        {
            if (ModelState.IsValid)
            {
                model.id_usuario = SessionHelper.Leer<int>("id_usuario");
                model.fec_pedido = DateTime.Now;

                model.crearPedido();

                return Redirect("~/Pedido/Mantenimiento/" + model.id_pedido);
            }
            else
            {
                ViewBag.TotalIng = pedido.obtenerTotalIng(model.id_dieta);
                ViewBag.dieta = pedido.obtenerDieta(model.id_dieta, SessionHelper.Leer<int>("id_usuario"));
                return View("~/Views/Pedido/Dieta.cshtml", model);
            }
        }

        public ActionResult Mantenimiento(int id)
        {
            if (pedido.validarPedido(id))
            {
                var ped = pedido.obtenerPedido(id);
                ViewBag.ingredientes = pedido_producto.obtenerIngRes(ped.id_dieta, ped.id_pedido);
                return View(ped);
            }
            else
            {
                return Redirect("~/Home/");
            }
        }

        public PartialViewResult Producto(int id, string id_dieta, string id_pedido)
        {
            ViewBag.ingrediente = pedido_producto.obtenerIngrediente(id);
            ViewBag.ing_rec = pedido_producto.obtenerIngrediente(id, Convert.ToInt16(id_dieta));
            var ped_prod = new pedido_producto();
            ped_prod.id_pedido = Convert.ToInt16(id_pedido);
            return PartialView(new pedido_producto());
        }

        public PartialViewResult ProductoPedido(int id)
        {
            return PartialView(pedido_producto.obtenerPedido(id));
        }

        public ActionResult ConfirmarPedido(int id)
        {
            var ped = pedido.obtenerPedido(id);
            ped.confirmarPedido();

            return Redirect("~/Pedido/");
        }

        public ActionResult AgregarProducto(pedido_producto model)
        {
            var rm = new ResponseModel();
            
            if (ModelState.IsValid)
            {
                rm = model.guardar();

                if (rm.response)
                {
                    return Redirect("~/Pedido/Mantenimiento/" + model.id_pedido);
                }
            }

            return Json(rm);
        }

        public ActionResult QuitarProducto(int id, string pro)
        {
            var rm = new ResponseModel();
            var ped_pro = new pedido_producto();
            ped_pro.id_pedido = id;
            ped_pro.id_producto = Convert.ToInt16(ped_pro);

            if (ModelState.IsValid)
            {
                rm = ped_pro.eliminar();

                if (rm.response)
                {
                    return Redirect("~/Pedido/Mantenimiento/" + id);
                }
            }

            return Json(rm);
        }
    }
}
