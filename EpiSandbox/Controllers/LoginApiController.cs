using EpiSandbox.Models;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace EpiSandbox.Controllers
{
    public class LoginApiController : ApiController
    {
        // POST api/<controller>
        [System.Web.Http.Route("login")]
        [ValidateAntiForgeryToken]
        public object Post([FromBody] LoginModel loginModel)
        {
            var isValid = Membership.Provider.ValidateUser(loginModel.Login, loginModel.Password);

            if (isValid)
            {
                // TODO : remember me
                FormsAuthentication.SetAuthCookie(loginModel.Login, true);
                return new { success = true, message = "" };
            }

            return new { success = false, message = "Login failed for given login/password" };
        }
    }
}