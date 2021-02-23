using EpiSandbox.Models;
using EPiServer.Core;
using EPiServer.Search;
using EPiServer.Search.Queries.Lucene;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Linq;

namespace EpiSandbox.Data
{
    public class PageSearcher : IPageSearcher
    {
        private readonly ISampleGetter _sampleGetter;

        public PageSearcher(ISampleGetter sampleGetter)
        {
            _sampleGetter = sampleGetter;
        }

        public IEnumerable<SearchHit> SearchPages(string query, int pagingNumber, int pagingSize)
        {
            var pageDatas = FindPages(query, pagingNumber, pagingSize).ToArray();

            return pageDatas.Select(p => new SearchHit()
            {
                Headline = p.Name,
                Link = p.LinkURL,
                Sample = _sampleGetter.GetSample(p, query)
            });
        }

        private IEnumerable<PageData> FindPages(string searchQuery, int pagingNumber, int pagingSize)
        {
            GroupQuery groupQuery = new GroupQuery(LuceneOperator.AND);
            groupQuery.QueryExpressions.Add(new ContentQuery<PageData>());

            groupQuery.QueryExpressions.Add(new FieldQuery(searchQuery));

            VirtualPathQuery pathQuery = new VirtualPathQuery();
            pathQuery.AddContentNodes(ContentReference.StartPage);
            groupQuery.QueryExpressions.Add(pathQuery);

            // The access control list query will remove any pages the user doesn't have read access to
            //AccessControlListQuery aclQuery = new AccessControlListQuery();
            //aclQuery.AddAclForUser(PrincipalInfo.Current, HttpContext.Current);
            //groupQuery.QueryExpressions.Add(aclQuery);

            var searchHandler = ServiceLocator.Current.GetInstance<SearchHandler>();
            SearchResults results = searchHandler.GetSearchResults(groupQuery, pagingNumber, pagingSize);

            var contentSearchHandler = ServiceLocator.Current.GetInstance<ContentSearchHandler>();
            foreach (var hit in results.IndexResponseItems)
            {
                yield return contentSearchHandler.GetContent<PageData>(hit);
            }
        }
    }
}