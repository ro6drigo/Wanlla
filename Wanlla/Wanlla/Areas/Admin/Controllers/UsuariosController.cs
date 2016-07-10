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

        public ActionResult Detalle(int id)
        {
            return View(usuario.obtener(id));
        }

        public ActionResult Mantenimiento(int id = 0)
        {
            return View(
                id == 0 ? new usuario() //nuevo usuario
                        : usuario.obtener(id)
            );
        }

        public ActionResult Guardar(usuario model)
        {
            if (ModelState.IsValid)
            {
                model.mantenimiento();
                return Redirect("~/Admin/Usuarios/");
            }
            else
            {
                return View("~/Admin/Usuarios/Mantenimiento.cshtml", model);
            }

        }
    }
}