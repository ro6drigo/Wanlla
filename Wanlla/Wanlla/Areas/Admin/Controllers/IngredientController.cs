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
    public class IngredientController : Controller
    {
        private ingrediente ingrediente = new ingrediente();
        // GET: Admin/Ingredient
        public ActionResult Index(string buscar = "")
        {
            if (buscar == "")
            {
                return View(ingrediente.listar());
            }
            else
            {
                return View(ingrediente.buscar(buscar));
            }
        }

        public JsonResult CargarIngrediente(AnexGRID grid)
        {
            return Json(ingrediente.ListarGrilla(grid));
        }

        public ActionResult Detalle(int id)
        {
            return View(ingrediente.Obtener(id));
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new ingrediente() //nuevo ingrediente
                        : ingrediente.Obtener(id)
            );
        }

        public ActionResult Guardar(ingrediente model)
        {
            if (ModelState.IsValid)
            {
                model.mantenimiento();
                return Redirect("~/Admin/Ingredient/");
            }
            else
            {
                return View("~/Admin/Ingredient/Mantenimiento.cshtml", model);
            }

        }
        
        public ActionResult Eliminar(int id)
        {
            ingrediente.id_ingrediente = id;
            ingrediente.eliminar();
            return Redirect("~/Admin/Ingredient/");
        }
    }
}