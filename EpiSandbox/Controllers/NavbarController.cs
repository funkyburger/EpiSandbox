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

        public ActionResult Index(PageData currentPage)
        {
            var model = new NavBarModel() {
                Links = _linkService.FetchNavbarLinks(currentPage),
                IsAuthenticated = User.Identity.IsAuthenticated,
                AuthenticatedAs = User.Identity.IsAuthenticated ? User.Identity.Name : null
            };

            return View("~/Views/Shared/_navbar.cshtml", model);
        }
    }
}