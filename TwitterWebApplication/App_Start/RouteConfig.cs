using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TwitterWebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            /*** Angular routes ***/
            Dictionary<string, string> angularRoutes = new Dictionary<string, string>();
            angularRoutes["AngularHomePage"] = "angular/homepage";
            angularRoutes["AngularTwitterwall"] = "angular/twitterwall";

            foreach (KeyValuePair<string, string> entry in angularRoutes)
            {
                routes.MapRoute(
                    name: entry.Key,
                    url: entry.Value,
                    defaults: new
                    {
                        controller = "Home",
                        action = "TwitterBackupHomePage"
                    }
                );
            }
            /*** Angular routes ***/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
