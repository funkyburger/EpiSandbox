using System.Web.Mvc;
using System.Web.Optimization;

namespace EpiSandbox
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterBundles();
        }

        private void RegisterBundles()
        {
            var bundles = BundleTable.Bundles;

            bundles.Add(new StyleBundle("~/css")
                .Include("~/Content/bootstrap.css",
                    "~/Content/bootstrap-grid.css"));

            bundles.Add(new StyleBundle("~/script")
                .Include("~/Scripts/jquery-3.0.0.js",
                "~/Scripts/bootstrap.bundle.js",
                "~/Scripts/modernizr-2.8.3.js"));
        }
    }
}