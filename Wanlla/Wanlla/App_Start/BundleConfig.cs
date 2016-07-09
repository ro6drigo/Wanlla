using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Wanlla.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-2.2.3.js", //JQuery 2.2.3
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/own-javascript.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/FancyZoom.js",
                        "~/Scripts/FancyZoomHTML.js",
                        "~/Scripts/jquery.anexgrid.min.js")); //JS Bootstrap 3.3.6

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css", //CSS Bootstrap 3.3.6
                      "~/Content/font-awesome.min.css", //Font Awesome
                      "~/Content/own-style.css", //Own Styles (POR MINIMIZAR)
                      "~/Content/sticky-footer.css")); //Sticky Footer (POR MINIMIZAR)
        }
    }
}