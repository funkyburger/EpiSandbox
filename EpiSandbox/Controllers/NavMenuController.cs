using EpiSandbox.Models.Pages;
using EpiSandbox.Models.View;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpiSandbox.Controllers
{
    public class NavMenuController : Controller
    {
        private readonly IContentRepository _contentRepository;
        private readonly IUrlResolver _urlResolver;

        public NavMenuController(IContentRepository contentRepository, IUrlResolver urlResolver)
        {
            _contentRepository = contentRepository;
            _urlResolver = urlResolver;
        }

        public ActionResult Index(PageData currentPage)
        {
            var startpage = ContentReference.StartPage;

            var mainNodes = _contentRepository.GetChildren<NodePage>(startpage);
            var ancestors = _contentRepository.GetAncestors(currentPage.ContentLink);

            var activeMainNode = mainNodes.SingleOrDefault(mn => ancestors.Any(a => a.ContentGuid == mn.ContentGuid)
                                        || mn.ContentGuid == currentPage.ContentGuid);

            var model = new NavMenu()
            {
                HeadItems = mainNodes.Select(i =>
                    new NavMenuItem()
                    {
                        Label = i.Name,
                        Link = _urlResolver.GetUrl(i.ContentLink),
                        Active = i.ContentGuid == activeMainNode?.ContentGuid
                    })
            };

            if (activeMainNode != null)
            {
                var subItems = _contentRepository.GetChildren<PageData>(activeMainNode.ContentLink);

                model.SubItems = subItems.Select(i => new NavMenuItem()
                {
                    Label = i.Name,
                    Link = _urlResolver.GetUrl(i.ContentLink),
                    Active = ancestors.Any(a => a.ContentGuid == currentPage.ContentGuid)
                            || i.ContentGuid == currentPage.ContentGuid
                });
            }
            else
            {
                model.SubItems = new NavMenuItem[] { };
            }

            return View("~/Views/Shared/_navMenu.cshtml", model);
        }

        //private ContentReference ActiveMainNode(PageData currentPage, IEnumerable<PageData> mainNodes)
        //{
        //    var ancestors = _contentRepository.GetAncestors(currentPage.ContentLink);

        //    var machin = mainNodes.SingleOrDefault(mn => ancestors.Any(a => a.ContentGuid == mn.ContentGuid)
        //                                || mn.ContentGuid == currentPage.ContentGuid);

        //    //if (mainNodes.Any(mn => ancestors.Any(a => a.ContentGuid == mn.)))

        //    //if (ancestors.Any(a => a.ContentGuid == i.ContentGuid)
        //    //            || i.ContentGuid == currentPage.ContentGuid)
        //    //{

        //    //}
        //}
    }
}