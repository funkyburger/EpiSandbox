using System;
using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using System.Data;

namespace EpiSandbox.Data
{
    public static class UserCreator
    {
        public static void CreateUserIfDontExists(string username, string password, string email, IEnumerable<string> roles)
        {
            var membershipUser = Membership.GetUser(username);

            if(membershipUser != null)
            {
                return;
            }

            MembershipCreateStatus status;
            var user = Membership.CreateUser(username, password, email, null, null, true, out status);

            if(status != MembershipCreateStatus.Success)
            {
                throw new DataException($"User creation has status '{status}'.");
            }

            foreach(string role in roles)
            {
                CheckRoleExists(role);
            }

            Roles.AddUserToRoles(username, roles.ToArray());
        }

        public static void CreateUser(string username, string password, string email, IEnumerable<string> roles)
        {
            var membershipUser = Membership.GetUser(username);

            if (membershipUser != null)
            {
                throw new DuplicateNameException("User already exists.");
            }

            MembershipCreateStatus status;
            var user = Membership.CreateUser(username, password, email, null, null, true, out status);

            if (status != MembershipCreateStatus.Success)
            {
                throw new DataException($"User creation has status '{status}'.");
            }

            foreach (string role in roles)
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