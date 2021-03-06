﻿using System.Web;
using System.Web.Optimization;

namespace TwitterWebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/jquery-1.10.2.intellisense.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Structure/css").Include(
                      "~/Content/Structure/grids.css",
                      "~/Content/Structure/website.css"));

            bundles.Add(new StyleBundle("~/Presentation/css").Include(
                      "~/Content/Presentation/border-radius.css",
                      "~/Content/Presentation/gradients.css",
                      "~/Content/Presentation/icons.css",
                      "~/Content/Presentation/website.css"));

            /*
            bundles.Add(new StyleBundle("~/MediaQueries/css").Include(
                      "~/Content/MediaQueries/*.css"));
            */
        }
    }
}
