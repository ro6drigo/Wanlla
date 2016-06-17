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
    public class IngredientesController : Controller
    {
        /// <summary>
        /// Instancia la clase Ingrediente
        /// </summary>
        private ingrediente ingrediente = new ingrediente();
        // GET: Ingrediente
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(ingrediente.listar());
            }
            else
            {
                return View(ingrediente.buscar(criterio));
            }
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(ingrediente.ListarGrilla(grid));
        }

        public ActionResult Detalle(int id)
        {
            return View(ingrediente.Obtener(id));
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new ingrediente()
                        : ingrediente.Obtener(id)
                );
        }

        public ActionResult Guardar(ingrediente model)
        {
            if (ModelState.IsValid)
            {
                model.mantenimiento();
                return Redirect("~/Admin/Ingrediente/Index");
            }
            else
            {
                return View("~/Admin/Views/Ingrediente/Mantenimiento.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            ingrediente.id_ingrediente = id;
            ingrediente.eliminar();
            return Redirect("~/Admin/Ingrediente/Index");
        }
    }
}
