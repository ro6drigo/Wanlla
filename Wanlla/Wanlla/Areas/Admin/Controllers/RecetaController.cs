using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Wanlla.Filters;

namespace Wanlla.Controllers
{
    //[Autenticado]
    public class RecetaController : Controller
    {
        /// <summary>
        /// Instancia la clase Receta
        /// </summary>
        private receta receta = new receta();
        // GET: Receta
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(receta.Listar());
            }
            else
            {
                return View(receta.buscar(criterio));
            }
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new receta()
                        : receta.Obtener(id)
                );
        }

        public ActionResult Guardar(receta model)
        {
            if (ModelState.IsValid)
            {
                model.mantenimiento();
                return Redirect("~/Receta/Index");
            }
            else
            {
                return View("~/Views/Receta/Mantenimiento.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            receta.id_receta = id;
            receta.eliminar();
            return Redirect("~/Receta/Index");
        }
    }
}