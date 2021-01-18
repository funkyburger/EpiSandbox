using EpiSandbox.Data;
using EpiSandbox.Models.View;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpiSandbox.Controllers
{
    public class NavbarController : Controller
    {
        private readonly ILinkService _linkService;

        public NavbarController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        // GET: NavBar
        public ActionResult Index(PageData currentPage)
        {
            var model = new NavBarModel() {
                Links = _linkService.FetchNavbarLinks(currentPage)
            };

            //return View("_navbar");
            return View("~/Views/Shared/_navbar.cshtml", model);
        }

        //private IDictionary<LinkModel, IEnumerable<LinkModel>> FetchLinks()
        //{

        //}
    }
}