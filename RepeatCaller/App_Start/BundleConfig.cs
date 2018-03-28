using System.Web.Optimization;

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
            bundles.Add(new ScriptBundle("~/scripts/Login").Include("~/Scripts/webCode/login.js"));
            bundles.Add(new ScriptBundle("~/scripts/Usuarios").Include("~/Scripts/webCode/usuarios.js"));
            bundles.Add(new StyleBundle("~/styles/Usuarios").Include("~/Content/css/usuarios.css"));
            bundles.Add(new ScriptBundle("~/scripts/Campanias").Include("~/Scripts/webCode/Campanias.js"));
            bundles.Add(new ScriptBundle("~/scripts/SubidaBase").Include("~/Scripts/webCode/subidaBase.js"));
            bundles.Add(new StyleBundle("~/Content/SubidaBase").Include("~/Content/css/subidaBase.css"));
            bundles.Add(new ScriptBundle("~/scripts/Reporte").Include("~/Scripts/webCode/reporte.js"));
            BundleTable.EnableOptimizations = true;
        }
    }
}