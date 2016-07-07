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
        private receta receta = new receta();
        private dieta_receta dieta_receta = new dieta_receta();

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

        public ActionResult AgregarReceta(int id = 0)
        {
            if (id == 0 || receta.Obtener(id) == null)
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else
            {
                ViewBag.comboDietas = dieta.listar(SessionHelper.Leer<int>("id_usuario"));
                ViewBag.receta = receta.Obtener(id);
                return View(new dieta_receta());
            }
        }

        public ActionResult EditarReceta(int id, int rec)
        {
            if (id == 0 || dieta_receta.obtener(id, rec) == null)
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else
            {
                ViewBag.comboDietas = dieta.listar(SessionHelper.Leer<int>("id_usuario"));
                return View(dieta_receta.obtener(id, rec));
            }
        }

        public ActionResult GuardarReceta(dieta_receta model)
        {
            if (ModelState.IsValid)
            {
                if (model.validarDietaReceta())
                {
                    model.guardar();
                    return Redirect("~/Dieta/Detalle/" + model.id_dieta);
                }
                else
                {
                    ViewBag.comboDietas = dieta.listar(SessionHelper.Leer<int>("id_usuario"));
                    ViewBag.receta = receta.Obtener(model.id_receta);
                    ModelState.AddModelError("id_dieta", "Esta dieta ya tiene este plato agregado");
                    return View("~/Views/Dieta/AgregarReceta.cshtml", model);
                }
                
            }
            else
            {
                return View("~/Views/Dieta/AgregarReceta.cshtml", model);
            }
        }

        public ActionResult GuardarEReceta(dieta_receta model)
        {
            if (ModelState.IsValid)
            {
                //if (model.validarDietaReceta())
                //{
                    model.actualizar();
                    return Redirect("~/Dieta/Detalle/" + model.id_dieta);
                //}
                //else
                //{
                //    ViewBag.comboDietas = dieta.listar(SessionHelper.Leer<int>("id_usuario"));
                //    ModelState.AddModelError("id_dieta", "Esta dieta ya tiene este plato agregado");
                //    return View("~/Views/Dieta/AgregarReceta.cshtml", model);
                //}

            }
            else
            {
                return View("~/Views/Dieta/AgregarReceta.cshtml", model);
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

        public ActionResult EliminarReceta(int id, int rec)
        {
            dieta_receta.id_dieta = id;
            dieta_receta.id_receta = rec;
            dieta_receta.eliminar();
            return Redirect("~/Dieta/Detalle/" + id);
        }
    }
}