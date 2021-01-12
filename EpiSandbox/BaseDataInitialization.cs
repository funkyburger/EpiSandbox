using EpiSandbox.Data;
using EpiSandbox.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System;
using System.Linq;

namespace EpiSandbox
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class BaseDataInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            InitializeAdmin();
            CreateDefaultHomePage();
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }

        private void InitializeAdmin()
        {
            var roles = new string[] { "Administrators", "WebAdmins", "WebEditors" };
            RoleCreator.CreateIfDontExsist(roles);

            UserCreator.CreateUserIfDontExists("admin", "P1p1K4k4", roles);
        }

        private void CreateDefaultHomePage()
        {
            var contentRepository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentRepository>();

            var root = ContentReference.RootPage;

            if (contentRepository.GetChildren<HomePage>(root).Any())
            {
                return;
            }

            var homepage = contentRepository.GetDefault<HomePage>(root);
            homepage.CreatedBy = "admin";
            homepage.Name = "Home";

            contentRepository.Save(homepage, EPiServer.DataAccess.SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);
        }
    }
}