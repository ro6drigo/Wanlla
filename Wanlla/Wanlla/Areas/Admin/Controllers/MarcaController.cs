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
    public class MarcaController : Controller
    {
        // GET: Admin/Marca
        private marca marca = new marca();

        public ActionResult Index(string buscar = "")
        {
            if (buscar == "")
            {
                return View(marca.Listar());
            }
            else
            {
                return View(marca.Buscar(buscar));
            }
        }

        public JsonResult CargarMarca(AnexGRID grid)
        {
            return Json(marca.ListarGrilla(grid));
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new marca() //nueva marca
                        : marca.Obtener(id)
            );
        }

        public ActionResult Guardar(marca model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Admin/Marca/");
            }
            else
            {
                return View("~/Admin/Marca/Mantenimiento.cshtml", model);
            }

        }

        public ActionResult Eliminar(int id)
        {
            marca.id_marca = id;
            marca.Eliminar();
            return Redirect("~/Admin/Marca/");
        }

    }
}