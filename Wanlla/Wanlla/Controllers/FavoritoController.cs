using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wanlla.Filters;

namespace Wanlla.Controllers
{
    [Autenticado]
    public class FavoritoController : Controller
    {
        // GET: Favorito
        public ActionResult Index()
        {
            return View();
        }
    }
}