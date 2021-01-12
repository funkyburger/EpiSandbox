using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EpiSandbox.Data
{
    public static class RoleCreator
    {
        public static void CreateIfDontExsist(string roleName)
        {
            if (!Roles.RoleExists(roleName))
            {
                Roles.CreateRole(roleName);
            }
        }

        public static void CreateIfDontExsist(IEnumerable<string> roleNames)
        {
            foreach (string roleName in roleNames)
            {
                CreateIfDontExsist(roleName);
            }
        }
    }
}