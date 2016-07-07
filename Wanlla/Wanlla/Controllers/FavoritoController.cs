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

        pu
    }
}