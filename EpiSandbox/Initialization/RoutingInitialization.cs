using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace EpiSandbox.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class RoutingInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //Add initialization logic, this method is called once after CMS has been initialized
            var routes = RouteTable.Routes;
            routes.MapMvcAttributeRoutes(new InheritedDirectRouteProvider());
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }

    public class InheritedDirectRouteProvider : DefaultDirectRouteProvider
    {
        public static List<string> controllers = new List<string>();

        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor.ControllerDescriptor.ControllerName == "NavMenu")
            {

            }

            controllers.Add(actionDescriptor.ControllerDescriptor.ControllerName);

            return actionDescriptor.GetCustomAttributes(typeof(IDirectRouteFactory), true).Cast<IDirectRouteFactory>().ToArray();
        }
    }
}