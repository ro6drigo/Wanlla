using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Wanlla.Controllers
{
    public class RecetaController : Controller
    {
        /// <summary>
        /// Instancia la clase Receta
        /// </summary>
        private receta receta = new receta();
        // GET: Receta
        public ActionResult Index(string nom_receta,string des_receta, int time_receta)
        {
            if (nom_receta == null || nom_receta == "")
            {
                return View(receta.Listar());
            }
            else
            {
                return View(receta.buscar(nom_receta, des_receta, time_receta));
            }
        }

        public ActionResult Detalle(int id)
        {
            return View(receta.Obtener(id));
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

        public ActionResult Buscar(string nom_receta, string des_receta, int time_receta)
        {
            return View(
                    nom_receta == null || nom_receta == "" ? receta.Listar()
                    : receta.buscar(nom_receta, des_receta, time_receta)
                    );
        }
    }
}