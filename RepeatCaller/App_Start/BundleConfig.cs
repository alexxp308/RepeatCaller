﻿using System.Web.Optimization;

namespace RepeatCaller
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/jquery-1.10.2.min.js",
                        "~/Scripts/lib/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css/siteStyle").Include(
                      "~/Content/css/lib/bootstrap.min.css",
                      "~/Content/css/lib/font-awesome.min.css",
                      "~/Content/css/Site.css"));

            bundles.Add(new ScriptBundle("~/scripts/Web").Include("~/Scripts/app/cookie.js", "~/Scripts/app/web.js"));
            bundles.Add(new StyleBundle("~/styles/Vendor").Include("~/Content/css/lib/vendor/util.css","~/Content/css/lib/vendor/main.css"));
            bundles.Add(new ScriptBundle("~/scripts/Vendor").Include("~/Scripts/lib/vendor/main.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}