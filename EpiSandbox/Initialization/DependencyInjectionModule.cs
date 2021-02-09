using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.WebApi;

namespace EpiSandbox.Initialization
{
    [InitializableModule]
    public class DependencyInjectionModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            var container = new UnityContainer();
            container.RegisterType<Data.ILinkService, Data.LinkService>();
            container.RegisterType<Data.IPageSearcher, Data.PageSearcher>();

            //GlobalConfiguration.Configuration.DependencyResolver
            //GlobalConfiguration.Configuration.DependencyResolver = 
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            // TODO One container to rule them all
            //DependencyResolver.SetResolver(container);

            context.Services.AddTransient<Data.ILinkService, Data.LinkService>();
            context.Services.AddTransient<Data.IPageSearcher, Data.PageSearcher>();
        }

        public void Initialize(InitializationEngine context)
        {
            DependencyResolver.SetResolver(new ServiceLocatorDependencyResolver(context.Locate.Advanced));
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }

    //public class Blah
    //{

    //}
}