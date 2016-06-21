using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Modelo;
using System.Web.Mvc;

namespace Wanlla.Filters
{
    public class AutenticadoAttribute : ActionFilterAttribute //AdminFilters
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExisteSesion())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", Action = "Index" }));
            }
        }
    }

    public class AutenticadoAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExisteSesion() || SessionHelper.Leer<string>("tipo_usuario") != "Admin")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "LoginAdmin", Action = "Index" }));
            }
        }
    }

    public class NoLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Home", Action = "Index" }));
            }
        }
    }
}