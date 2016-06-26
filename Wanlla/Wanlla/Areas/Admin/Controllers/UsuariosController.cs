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
        public ActionResult Index(string criterio)
        {
            if (criterio == null | criterio == "")
            {
                return View(usuario.Listar());
            }
            else
            {
                return View(usuario.buscar(criterio));
            }
        }
    }
}