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
    public class UsuariosController : Controller
    {
        private usuario usuario = new usuario();
        // GET: Admin/Usuarios
        public ActionResult Index(string buscar = "")
        {
            if (buscar == "")
            {
                return View(usuario.Listar());
            }
            else
            {
                return View(usuario.buscar(buscar));
            }
        }

        public JsonResult CargarUsuario(AnexGRID grid)
        {
            return Json(usuario.ListarGrilla(grid));
        }

        public ActionResult Consulta()
        {
            return View(usuario.Consulta());
        }
    }
}