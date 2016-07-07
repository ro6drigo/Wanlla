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
    public class FavoritoController : Controller
    {
        private favorito favorito = new favorito();

        // GET: Favorito
        public ActionResult Index(string buscar = "")
        {
            int idusuario = SessionHelper.Leer<int>("id_usuario");

            return View(favorito.Listar(idusuario));
        }

        public ActionResult GuardarFavorito(favorito model)
        {
            if (ModelState.IsValid)
            {
                model.id_usuario = SessionHelper.Leer<int>("id_usuario");
                model.Guardar();
                return Redirect("~/Home");
            }
            else
            {
                return View("~/Home", model);
            }
        }
        public ActionResult Eliminar(int id)
        {
            favorito.Eliminar();
            return Redirect("~/Favorito");
        }
    }
}