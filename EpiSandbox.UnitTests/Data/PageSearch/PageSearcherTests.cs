using EpiSandbox.Data;
using EpiSandbox.Data.PageSearch;
using EpiSandbox.Models.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EpiSandbox.UnitTests.Data.PageSearch
{
    [TestClass]
    public class PageSearcherTests
    {
        [TestMethod]
        public void SearcherReturnsConvertedResults()
        {
            var internalPageSearcher = new Mock<IInternalPageSearcher>();
            internalPageSearcher.Setup(s => s.SearchPages(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new IContentPage[] { 
                    new TestContent("headline",
                            "/link/to/page",
                            "<div id=\"lipsum\">"
                                + "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue. Aenean aliquam feugiat suscipit. Nunc ultricies tortor vel augue viverra, sit amet tincidunt lectus faucibus. Aliquam vel aliquet purus. Nulla sed suscipit lorem. Nulla aliquam neque et massa dictum rhoncus non id enim. Suspendisse potenti. Cras dui ex, dapibus eu diam nec, convallis volutpat felis. Morbi convallis leo at diam placerat, vitae rutrum mauris dictum.</p>"
                                + "<p>Aliquam et fermentum leo. Curabitur odio nibh, convallis vel magna id, aliquam sagittis nisl. Pellentesque in purus fringilla purus tempor scelerisque. Nunc vestibulum euismod nisl, eu tincidunt sem posuere id. Vivamus non massa venenatis, ullamcorper lacus id, viverra elit. Aenean maximus eleifend augue, vel volutpat diam mattis sed. Morbi porttitor ipsum augue, sit amet sodales tellus pellentesque eu. Phasellus egestas quis est non lobortis. Nam ipsum nulla, mollis at sodales quis, consequat rutrum metus. Curabitur consequat nulla a porttitor venenatis. Aliquam commodo mattis ligula id laoreet. Etiam ullamcorper tellus nec ullamcorper ultricies. Nam ac auctor mauris. Nunc vel tincidunt quam, at posuere erat. Ut id porta velit.</p>"
                                + "<p>Sed finibus rhoncus diam quis ultrices. Fusce est est, suscipit ac nisl ut, auctor semper ligula. Curabitur aliquam ornare nisi ac finibus. Proin dui justo, tincidunt at dictum quis, pulvinar volutpat lacus. Praesent fringilla rutrum dui id volutpat. Quisque lacinia magna sit amet ligula viverra, nec molestie diam accumsan. In efficitur nisi felis, sed fermentum justo varius eu. Nulla fringilla a eros nec maximus.</p>"
                                + "<p>In vulputate, quam placerat pellentesque hendrerit, felis massa blandit augue, vitae lobortis leo diam id libero. Cras porttitor metus vitae lorem faucibus, non vestibulum nulla placerat. Vivamus gravida mauris eu ante hendrerit imperdiet. Praesent malesuada non nunc pellentesque ultricies. Suspendisse finibus diam in purus vestibulum bibendum. Donec posuere diam sit amet fringilla condimentum. Nulla id sem sapien. Vestibulum sagittis ante quis quam lacinia interdum. Nulla faucibus nisl eget consectetur tristique. Donec eget arcu tortor. Nulla eu tellus ac urna ultricies aliquet a non metus. Aliquam lorem sapien, sollicitudin at augue id, ultrices posuere mi.</p>"
                                + "<p>In vestibulum, orci ut tempus laoreet, massa eros scelerisque velit, ac elementum tellus urna ut mauris. Integer eu lobortis erat. Integer vitae ultricies lectus, vitae pulvinar nunc. Vestibulum varius volutpat ligula et accumsan. Etiam feugiat a libero eget porttitor. In non placerat tortor, a ultrices massa. Aliquam malesuada urna vitae libero lobortis tincidunt. Sed vel pulvinar ipsum, non vestibulum nunc. Proin sit amet enim nec sem placerat tempus in sed risus.</p>"
                                + "</div>") 
                });

            var queryParser = new Mock<IQueryParser>();

            var pageSearcher = new PageSearcher(internalPageSearcher.Object, queryParser.Object);

            var result = pageSearcher.SearchPages(new Query(), 0, 10).Single();
            result.Headline.ShouldBe("headline");
            result.Link.ShouldBe("/link/to/page");
            result.Content.ShouldBe(
                new string[] { 
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue. Aenean aliquam feugiat suscipit. Nunc ultricies tortor vel augue viverra, sit amet tincidunt lectus faucibus. Aliquam vel aliquet purus. Nulla sed suscipit lorem. Nulla aliquam neque et massa dictum rhoncus non id enim. Suspendisse potenti. Cras dui ex, dapibus eu diam nec, convallis volutpat felis. Morbi convallis leo at diam placerat, vitae rutrum mauris dictum.",
                    "Aliquam et fermentum leo. Curabitur odio nibh, convallis vel magna id, aliquam sagittis nisl. Pellentesque in purus fringilla purus tempor scelerisque. Nunc vestibulum euismod nisl, eu tincidunt sem posuere id. Vivamus non massa venenatis, ullamcorper lacus id, viverra elit. Aenean maximus eleifend augue, vel volutpat diam mattis sed. Morbi porttitor ipsum augue, sit amet sodales tellus pellentesque eu. Phasellus egestas quis est non lobortis. Nam ipsum nulla, mollis at sodales quis, consequat rutrum metus. Curabitur consequat nulla a porttitor venenatis. Aliquam commodo mattis ligula id laoreet. Etiam ullamcorper tellus nec ullamcorper ultricies. Nam ac auctor mauris. Nunc vel tincidunt quam, at posuere erat. Ut id porta velit.",
                    "Sed finibus rhoncus diam quis ultrices. Fusce est est, suscipit ac nisl ut, auctor semper ligula. Curabitur aliquam ornare nisi ac finibus. Proin dui justo, tincidunt at dictum quis, pulvinar volutpat lacus. Praesent fringilla rutrum dui id volutpat. Quisque lacinia magna sit amet ligula viverra, nec molestie diam accumsan. In efficitur nisi felis, sed fermentum justo varius eu. Nulla fringilla a eros nec maximus.",
                    "In vulputate, quam placerat pellentesque hendrerit, felis massa blandit augue, vitae lobortis leo diam id libero. Cras porttitor metus vitae lorem faucibus, non vestibulum nulla placerat. Vivamus gravida mauris eu ante hendrerit imperdiet. Praesent malesuada non nunc pellentesque ultricies. Suspendisse finibus diam in purus vestibulum bibendum. Donec posuere diam sit amet fringilla condimentum. Nulla id sem sapien. Vestibulum sagittis ante quis quam lacinia interdum. Nulla faucibus nisl eget consectetur tristique. Donec eget arcu tortor. Nulla eu tellus ac urna ultricies aliquet a non metus. Aliquam lorem sapien, sollicitudin at augue id, ultrices posuere mi.",
                    "In vestibulum, orci ut tempus laoreet, massa eros scelerisque velit, ac elementum tellus urna ut mauris. Integer eu lobortis erat. Integer vitae ultricies lectus, vitae pulvinar nunc. Vestibulum varius volutpat ligula et accumsan. Etiam feugiat a libero eget porttitor. In non placerat tortor, a ultrices massa. Aliquam malesuada urna vitae libero lobortis tincidunt. Sed vel pulvinar ipsum, non vestibulum nunc. Proin sit amet enim nec sem placerat tempus in sed risus."
                });
        }

        [TestMethod]
        public void SearcherHandlesMultipleContent()
        {
            var internalPageSearcher = new Mock<IInternalPageSearcher>();
            internalPageSearcher.Setup(s => s.SearchPages(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new IContentPage[] {
                    new TestContent("headline",
                            "/link/to/page",
                                new string[] { "<p>content 1</p><p>content 2</p>"
                                    , "<p>content 3</p><p>content 4</p>" })
                });

            var queryParser = new Mock<IQueryParser>();

            var pageSearcher = new PageSearcher(internalPageSearcher.Object, queryParser.Object);

            var result = pageSearcher.SearchPages(new Query(), 0, 10).Single();
            result.Headline.ShouldBe("headline");
            result.Link.ShouldBe("/link/to/page");
            result.Content.ShouldBe(
                new string[] {
                    "content 1",
                    "content 2",
                    "content 3",
                    "content 4"                    
                });
        }

        private class TestContent : IContentPage
        {
            public string Headline { get; set; }
            public string Link { get; set; }
            public IEnumerable<string> Content { get; set; }

            public TestContent(string headline, string link, string content)
            {
                Headline = headline;
                Link = link;
                Content = new string[] { content };
            }

            public TestContent(string headline, string link, IEnumerable<string> content)
            {
                Headline = headline;
                Link = link;
                Content = content;
            }
        }
    }
}
