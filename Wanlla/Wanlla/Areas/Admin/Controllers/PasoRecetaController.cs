using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wanlla.Filters;

namespace Wanlla.Areas.Admin.Controllers
{
    [AutenticadoAdmin]
    public class PasoRecetaController : Controller
    {
        private paso_receta pasoreceta = new paso_receta();
        // GET: Admin/PasoReceta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            if(id != 0)
            {
                return View( new paso_receta());
            }
            else
            {
                return View();
            }
        }

        public ActionResult AgregarPaso(int id = 0)
        {
            if (id != 0)
            {
                ViewBag.id = id;
                return View(new paso_receta());
            }
            else
            {
                return View();
            }
        }

        public ActionResult Guardar(paso_receta model)
        {
            if (ModelState.IsValid)
            {
                model.agregar();
                return Redirect("~/Admin/Receta/");
            }
            else
            {
                return View("~/Admin/PasoReceta/AgregarPaso.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            pasoreceta.id_paso = id;
            pasoreceta.eliminar();
            return Redirect("~/Admin/Receta/");
        }
    }
}