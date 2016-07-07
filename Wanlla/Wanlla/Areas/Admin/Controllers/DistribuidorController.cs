using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;
using Wanlla.Filters;

namespace Wanlla.Areas.Admin.Controllers
{
    [AutenticadoAdmin]
    public class DistribuidorController : Controller
    {
        // GET: Admin/Distribuidor
        private distribuidor distribuidor = new distribuidor();

        public ActionResult Index(string buscar = "")
        {
            if (buscar == "")
            {
                return View(distribuidor.Listar());
            }
            else
            {
                return View(distribuidor.Buscar(buscar));
            }
        }

        public JsonResult CargarDistribuidor(AnexGRID grid)
        {
            return Json(distribuidor.ListarGrilla(grid));
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new distribuidor() //nueva distribuidor
                        : distribuidor.Obtener(id)
            );
        }

        public ActionResult Detalle(int id)
        {
            return View(distribuidor.Obtener(id));
        }

        public ActionResult Guardar(distribuidor model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Admin/Distribuidor/");
            }
            else
            {
                return View("~/Admin/Distribuidor/Mantenimiento.cshtml", model);
            }

        }

        public ActionResult Eliminar(int id)
        {
            distribuidor.id_distribuidor = id;
            distribuidor.Eliminar();
            return Redirect("~/Admin/Distribuidor/");
        }
    }
}