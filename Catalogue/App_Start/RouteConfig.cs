using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Catalogue
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{lang}/{controller}/{action}/{id}",
                new
                {
                    lang = "en",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
           );
        }
    }
}