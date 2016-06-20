using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wanlla.Filters;

namespace Wanlla.Areas.Admin.Controllers
{
    //[Autenticado]
    public class RecetaController : Controller
    {
        private receta receta = new receta();
        // GET: Admin/Receta
        public ActionResult Index()
        {
            return View();
        }
    }
}