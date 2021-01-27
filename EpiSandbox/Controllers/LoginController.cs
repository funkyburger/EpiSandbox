using EpiSandbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpiSandbox.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var model = new AuthenticationModel();

            if (User.Identity.IsAuthenticated)
            {
                model.Login = User.Identity.Name;

                return View("LoggedIn", model);
            }

            return View("Login", model);
        }
    }
}