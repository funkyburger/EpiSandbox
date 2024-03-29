﻿using EpiSandbox.Data;
using EpiSandbox.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EpiSandbox.Initialization
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
            var roles = new List<string>();
            
            foreach(var role in Enum.GetValues(typeof(UserRoles)))
            {
                roles.Add(role.ToString());
            }

            RoleCreator.CreateIfDontExsist(roles);

            UserCreator.CreateUserIfDontExists("admin", "P1p1K4k4", null, roles);
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