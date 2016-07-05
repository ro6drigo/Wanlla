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
            return View(
                id == 0 ? new paso_receta() //nueva paso receta
                        : paso_receta.Obtener(id)
            );
        }
    }
}