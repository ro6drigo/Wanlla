using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wanlla.Filters;

namespace Wanlla.Areas.Admin.Controllers
{
    //[Autenticado]
    public class ProductoController : Controller
    {
        private producto producto = new producto();
        // GET: Admin/Producto
        public ActionResult Index(string criterio)
        {
            if (criterio == null | criterio == "")
            {
                return View(producto.listar());
            }
            else
            {
                return View(producto.buscar(criterio));
            }
        }

        public ActionResult Detalle(int id)
        {
            return View(producto.Obtener(id));
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new producto() //nuevo producto
                        : producto.Obtener(id)
            );
        }

        public ActionResult Guardar(producto model)
        {
            if (ModelState.IsValid)
            {
                model.mantenimiento();
                return Redirect("~/Admin/Producto/");
            }
            else
            {
                return View("~/Admin/Producto/Mantenimiento.cshtml", model);
            }

        }

        public ActionResult Eliminar(int id)
        {
            producto.id_ingrediente = id;
            producto.eliminar();
            return Redirect("~/Admin/Producto/");
        }
    }
}