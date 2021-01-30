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
            BundleTable.EnableOptimizations = false;

            bundles.Add(new StyleBundle("~/css")
                .Include("~/Content/Css/bootstrap/bootstrap.css",
                    "~/Content/Css/bootstrap/bootstrap-grid.css"));

            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/Content/Scripts/modernizr-2.8.3.js")
                .Include("~/Content/Scripts/jquery/jquery-3.0.0.min.js")
                .Include("~/Content/Scripts/bootstrap/bootstrap.bundle.min.js")
                .Include("~/Content/Scripts/search.js"));
        }
    }
}