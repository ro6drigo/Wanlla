using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wanlla.Filters;
using Modelo;

namespace Wanlla.Controllers
{
    [Autenticado]
    public class DietaController : Controller
    {
        private dieta dieta = new dieta();

        // GET: Dieta
        public ActionResult Index(string buscar = "")
        {
            if (buscar == "")
            {
                return View(dieta.listar(SessionHelper.Leer<int>("id_usuario")));
            }
            else
            {
                return View(dieta.buscar(SessionHelper.Leer<int>("id_usuario"), buscar));
            }
        }

        public ActionResult Detalle(int id = 0)
        {
            if(id == 0 || dieta.detalle(id, SessionHelper.Leer<int>("id_usuario")) == null)
            {
                return Redirect("~/Dieta");
            }
            else
            {
                return View(dieta.detalle(id, SessionHelper.Leer<int>("id_usuario")));
            }
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                (id == 0) ? new dieta() //nueva dieta
                        : (dieta.obtener(id, SessionHelper.Leer<int>("id_usuario")) == null ?
                                new dieta()
                                : dieta.obtener(id, SessionHelper.Leer<int>("id_usuario")))
            );
        }

        public ActionResult Guardar(dieta model)
        {
            model.id_usuario = SessionHelper.Leer<int>("id_usuario");

            if (ModelState.IsValid)
            {
                model.mantenimiento();
                return Redirect("~/Dieta");
            }
            else
            {
                return View("~/Views/Dieta/Mantenimiento.cshtml", model);
            }

        }

        public ActionResult Eliminar(int id)
        {
            dieta.id_dieta = id;
            dieta.eliminar();
            return Redirect("~/Dieta");
        }
    }
}