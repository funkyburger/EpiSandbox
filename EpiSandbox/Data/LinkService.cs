using EpiSandbox.Models.Pages;
using EpiSandbox.Models.View;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiSandbox.Data
{
    public class LinkService : ILinkService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IUrlResolver _urlResolver;

        public LinkService(IContentRepository contentRepository, IUrlResolver urlResolver)
        {
            _contentRepository = contentRepository;
            _urlResolver = urlResolver;
        }

        public IDictionary<LinkModel, IEnumerable<LinkModel>> FetchNavbarLinks(PageData currentPage)
        {
            var startpage = ContentReference.StartPage;
            var result = new Dictionary<LinkModel, IEnumerable<LinkModel>>();

            var mainNodes = _contentRepository.GetChildren<NodePage>(startpage);
            var ancestors = currentPage != null ? _contentRepository.GetAncestors(currentPage.ContentLink)
                : new IContent[] { };

            foreach (var node in mainNodes)
            {
                var sublinks = new List<LinkModel>();

                foreach (var pageData in _contentRepository.GetChildren<PageData>(node.ContentLink))
                {
                    sublinks.Add(FromPageData(pageData, ancestors, currentPage));
                }

                result.Add(FromPageData(node, ancestors, currentPage), sublinks);
            }

            return result;
        }

        private LinkModel FromPageData(PageData pageData, IEnumerable<IContent> ancestors, PageData currentPage) 
        {
            return new LinkModel()
            {
                Label = pageData.Name,
                Link = _urlResolver.GetUrl(pageData.ContentLink),
                Active = ancestors.Any(a => a.ContentGuid == pageData.ContentGuid)
                    || (currentPage != null && pageData.ContentGuid == currentPage.ContentGuid)
            };
        }
    }
}