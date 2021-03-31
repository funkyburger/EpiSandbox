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
            container.RegisterType<Data.ISampleGetter, Data.SampleGetter>();
            container.RegisterType<Data.PageSearch.IInternalPageSearcher, Data.PageSearch.InternalPageSearcher>();
            container.RegisterType<Data.PageSearch.IQueryParser, Data.PageSearch.QueryParser>();

            //GlobalConfiguration.Configuration.DependencyResolver
            //GlobalConfiguration.Configuration.DependencyResolver = 
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            // TODO One container to rule them all
            //DependencyResolver.SetResolver(container);

            context.Services.AddTransient<Data.ILinkService, Data.LinkService>();
            context.Services.AddTransient<Data.ISampleGetter, Data.SampleGetter>();
            context.Services.AddTransient<Data.PageSearch.IInternalPageSearcher, Data.PageSearch.InternalPageSearcher>();
            context.Services.AddTransient<Data.PageSearch.IQueryParser, Data.PageSearch.QueryParser>();
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