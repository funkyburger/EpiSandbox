using System.Data;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using EpiSandbox.Data;
using EpiSandbox.Models;

namespace EpiSandbox.Controllers
{
    public class AuthenticationController : ApiController
    {
        [System.Web.Http.Route("login")]
        [ValidateAntiForgeryToken]
        public object Login([FromBody] AuthenticationModel model)
        {
            var isValid = Membership.Provider.ValidateUser(model.Login, model.Password);

            if (isValid)
            {
                // TODO : remember me ... or not
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return new { success = true, message = "" };
            }

            return new { success = false, message = "Login failed for given login/password" };
        }

        [System.Web.Http.Route("logoff")]
        [ValidateAntiForgeryToken]
        public object Logoff([FromBody] AuthenticationModel model)
        {
            FormsAuthentication.SignOut();
            return new { success = false, message = "" };
        }

        [System.Web.Http.Route("register")]
        [ValidateAntiForgeryToken]
        public object Register([FromBody] AuthenticationModel model)
        {
            if (model == null
                    || string.IsNullOrEmpty(model.Login)
                    || string.IsNullOrEmpty(model.Email)
                    || string.IsNullOrEmpty(model.Password)
                    || string.IsNullOrEmpty(model.PasswordConfirmation))
            {
                return new { success = false, message = "All fields must be filled" };
            }
            else if (model.Password != model.PasswordConfirmation)
            {
                return new { success = false, message = "Password and confirmation must match" };
            }

            try
            {
                UserCreator.CreateUser(model.Login, model.Password, model.Email, new string[] { UserRoles.WebUsers.ToString() });
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return new { success = true, message = "" };
            }
            catch (DuplicateNameException)
            {
                return new { success = false, message = "A user with that name already exists" };
            }
            catch (DataException)
            {
                return new { success = false, message = "A user could not be created with given data" };
            }
        }
    }
}