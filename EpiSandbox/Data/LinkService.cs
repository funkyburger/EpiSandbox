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

            foreach (var node in mainNodes)
            {
                //result.Add()
                var sublinks = new List<LinkModel>();

                foreach (var pageData in _contentRepository.GetChildren<PageData>(node.ContentLink))
                {
                    sublinks.Add(FromPageData(pageData));
                }

                result.Add(FromPageData(node), sublinks);
            }

            return result;
        }

        private LinkModel FromPageData(PageData pageData) 
        {
            return new LinkModel()
            {
                Label = pageData.Name,
                Link = _urlResolver.GetUrl(pageData.ContentLink),
                Active = false
            };
        }
    }
}