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
        // GET: Admin/IngredienteReceta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new ingrediente_receta() //nueva ingrediente receta
                        : ingredientereceta.Obtener(id)
            );
        }
    }
}