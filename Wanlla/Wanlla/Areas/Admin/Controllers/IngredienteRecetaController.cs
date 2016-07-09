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
    
    public class IngredienteRecetaController : Controller
    {
        private ingrediente_receta ingredientereceta = new ingrediente_receta();
        private ingrediente tipoingrediente = new ingrediente();
        // GET: Admin/IngredienteReceta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            if (id != 0)
            {
                ViewBag.tipoingrediente = tipoingrediente.ListarIngrediente();
                return View(new ingrediente_receta());
            }
            else
            {
                return View();
            }
        }

        public ActionResult AgregarIngrediente(int id = 0)
        {
            if (id != 0)
            {
                ViewBag.id = id;
                ViewBag.ingredientes = tipoingrediente.ListarIngrediente();
                return View(new ingrediente_receta());
            }
            else
            {
                return View();
            }
        }

        public ActionResult Guardar(ingrediente_receta model)
        {
            if (ModelState.IsValid)
            {
                
                model.agregar();
                return Redirect("~/Admin/Receta/");
            }
            else
            {
                return View("~/Admin/IngredienteReceta/AgregarIngrediente.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id)
        {
            ingredientereceta.id_receta = id;
            ingredientereceta.eliminar();
            return Redirect("~/Admin/Receta/");
        }
    }
}