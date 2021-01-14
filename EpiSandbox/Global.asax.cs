//using EPiServer.Web.Routing;
using EpiSandbox.Initialization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EpiSandbox
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Enable api routing
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RegisterBundles();
        }

        private void RegisterBundles()
        {
            var bundles = BundleTable.Bundles;

            // To force script refresh
            BundleTable.EnableOptimizations = true;

            bundles.Add(new StyleBundle("~/css")
                .Include("~/Content/bootstrap.css",
                    "~/Content/bootstrap-grid.css"));

            // To force episandbox.js refresh
            bundles.Add(new ScriptBundle("~/script")
                .Include("~/Scripts/jquery-3.0.0.js",
                "~/Scripts/bootstrap.bundle.js",
                "~/Scripts/modernizr-2.8.3.js"//,
                // TODO : put back
                                              //"~/Scripts/episandbox.js"));
                ));
        }
    }
}