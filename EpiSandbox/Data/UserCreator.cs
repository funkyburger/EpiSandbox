using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System;
using System.Linq;
using System.Configuration.Provider;
using System.Web.Security;
using EPiServer.Logging.Compatibility;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Fluent.Infrastructure.FluentModel;
//using System.Configuration.Provider;
//using System.Web.Security;
//using EPiServer.Framework;
//using EPiServer.Framework.Initialization;
//using EPiServer.Logging.Compatibility;
using EPiServer.ApplicationModules.Security;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.Collections.Generic;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Fluent.Infrastructure.FluentTools;

namespace EpiSandbox.Data
{
    public static class UserCreator
    {
        public static void CreateUserIfDontExists(string username, string password, IEnumerable<string> roles)
        {
            var membershipUser = Membership.GetUser(username);

            if(membershipUser != null)
            {
                return;
            }

            MembershipCreateStatus status;
            var user = Membership.CreateUser(username, password, null, null, null, true, out status);

            if(status != MembershipCreateStatus.Success)
            {
                throw new Exception($"User creation has status '{status}'.");
            }

            foreach(string role in roles)
            {
                CheckRoleExists(role);
            }

            Roles.AddUserToRoles(username, roles.ToArray());
        }

        private static void CheckRoleExists(string roleName)
        {
            if (!Roles.RoleExists(roleName)) 
            {
                throw new Exception($"Role '{roleName}' doesn't exist.");
            };
        }
    }
}