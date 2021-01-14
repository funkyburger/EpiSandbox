using EpiSandbox.Data;
using EpiSandbox.Models;
using System.Data;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace EpiSandbox.Controllers
{
    public class RegisterApiController : ApiController
    {
        [System.Web.Http.Route("register")]
        [ValidateAntiForgeryToken]
        public object Post([FromBody] RegisterModel registerModel)
        {
            if(registerModel == null
                    || string.IsNullOrEmpty(registerModel.Login)
                    || string.IsNullOrEmpty(registerModel.Email)
                    || string.IsNullOrEmpty(registerModel.Password)
                    || string.IsNullOrEmpty(registerModel.PasswordConfirmation))
            {
                return new { success = false, message = "All fields must be filled" };
            }
            else if(registerModel.Password != registerModel.PasswordConfirmation)
            {
                return new { success = false, message = "Password and confirmation must match" };
            }

            try
            {
                UserCreator.CreateUser(registerModel.Login, registerModel.Password, registerModel.Email, new string[] { UserRoles.WebUsers.ToString() });
                FormsAuthentication.SetAuthCookie(registerModel.Login, true);
                return new { success = true, message = "" };
            }
            catch(DuplicateNameException)
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