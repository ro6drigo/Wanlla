﻿using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wanlla.Filters;

namespace Wanlla.Areas.Admin.Controllers
{
    [AutenticadoAdmin]
    public class RecetaController : Controller
    {
        private receta receta = new receta();
        private categoria tipo = new categoria();
        // GET: Admin/Receta
        public ActionResult Index(string buscar = "")
        {
            if (buscar == "")
            {
                
                return View(receta.Listar());
            }
            else
            {
                return View(receta.buscar(buscar));
            }
        }

        public ActionResult Consulta()
        {
            return View(receta.Consulta());
        }

        public JsonResult CargarReceta(AnexGRID grid)
        {
            return Json(receta.ListarGrilla(grid));
        }

        public ActionResult Detalle(int id)
        {
            return View(receta.Obtener(id));
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            ViewBag.tipo = tipo.ListarCategoria();
            return View(
                id == 0 ? new receta() //nueva receta
                        : receta.Obtener(id)
            );
        }

        //public ActionResult Guardar(receta model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.mantenimiento();
        //        return Redirect("~/Admin/Receta/");
        //    }
        //    else
        //    {
        //        return View("~/Admin/Receta/Mantenimiento.cshtml", model);
        //    }

        //}

        public JsonResult Guardar(receta model, HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();
            string foto;
            ModelState.Remove("Password");
            if (model.img_receta != null)
            {
                foto = model.img_receta;
                System.IO.File.Delete(Server.MapPath("../images/") + foto);
            }
            if (!ModelState.IsValid)
            {
                rm = model.Guardar(Foto);
            }
            rm.href = Url.Content("~/Admin/Usuarios/Index");
            return Json(rm);
        }

        public ActionResult Eliminar(int id)
        {
            receta.id_receta = id;
            receta.eliminar();
            return Redirect("~/Admin/Receta/");
        }
    }
}