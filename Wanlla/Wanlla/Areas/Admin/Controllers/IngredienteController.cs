﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo;

namespace Wanlla.Controllers
{
    public class IngredienteController : Controller
    {
        /// <summary>
        /// Instancia la clase Ingrediente
        /// </summary>
        private ingrediente ingrediente = new ingrediente();
        // GET: Ingrediente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Mantenimiento()
        {
            return View();
        }
    }
}