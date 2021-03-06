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
    public class CategoriaController : Controller
    {
        private categoria categoria = new categoria();
        // GET: Admin/Categoria
        public ActionResult Index(string buscar = "")
        {
            if (buscar == "")
            {
                return View(categoria.listar());
            }
            else
            {
                return View(categoria.buscar(buscar));
            }
        }

        public JsonResult CargarCategoria(AnexGRID grid)
        {
            return Json(categoria.ListarGrilla(grid));
        }

        public ActionResult Detalle(int id)
        {
            return View(categoria.Obtener(id));
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new categoria() //nueva categoría
                        : categoria.Obtener(id)
            );
        }

        //public ActionResult Guardar(categoria model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.mantenimiento();
        //        return Redirect("~/Admin/Categoria/");
        //    }
        //    else
        //    {
        //        return View("~/Admin/Categoria/Mantenimiento.cshtml", model);
        //    }
        //}

        public JsonResult Guardar(categoria model, HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();
            string foto;
            ModelState.Remove("Password");
            if (model.img_categoria != null)
            {
                foto = model.img_categoria;
                System.IO.File.Delete(Server.MapPath("../images/") + foto);
            }
            if (!ModelState.IsValid)
            {
                rm = model.Guardar(Foto);
            }
            rm.href = Url.Content("~/Admin/Categoria/Index");
            return Json(rm);
        }

        public ActionResult Eliminar(int id)
        {
            categoria.id_categoria = id;
            categoria.eliminar();
            return Redirect("~/Admin/Categoria/");
        }
    }
}