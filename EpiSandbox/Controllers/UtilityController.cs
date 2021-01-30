using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpiSandbox.Controllers
{
    public class UtilityController : Controller
    {
        [Route("search")]
        public ActionResult Search(string query)
        {
            //http://localhost:65088/Utility/Search?query=bla

            return View();
        }
    }
}