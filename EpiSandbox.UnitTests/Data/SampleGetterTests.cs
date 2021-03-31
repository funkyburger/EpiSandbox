using EpiSandbox.Data;
using EpiSandbox.Data.PageSearch;
using EpiSandbox.Models.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;

namespace EpiSandbox.UnitTests.Data
{
    [TestClass]
    public class SampleGetterTests
    {
        [TestMethod]
        public void SamplerShowsOnlyReleventParagraph()
        {
            var sampleGetter = new SampleGetter();
            
            sampleGetter.GetSample(GenerateStandardResultTestPage(), 
                    new Query() { Fragments = new string[] { "dolor" } })
                .ShouldBe("Lorem ipsum <strong>dolor</strong> sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue. Aenean aliquam feugiat suscipit. Nunc ultricies tortor vel augue viverra, sit amet tincidunt lectus faucibus. Aliquam vel aliquet purus. Nulla sed suscipit lorem. Nulla aliquam neque et massa dictum rhoncus non id enim. Suspendisse potenti. Cras dui ex, dapibus eu diam nec, convallis volutpat felis. Morbi convallis leo at diam");
        }

        [TestMethod]
        public void SamplerHandlesNullContent()
        {
            var sampleGetter = new SampleGetter();

            sampleGetter.GetSample(GenerateResultTestPage((IEnumerable<string>)null),
                    new Query() { Fragments = new string[] { "dolor" } })
                .ShouldBeNull();
        }

        [TestMethod]
        public void SamplerHandlesEmptyContent()
        {
            var sampleGetter = new SampleGetter();

            sampleGetter.GetSample(GenerateResultTestPage(new string[] { }),
                    new Query() { Fragments = new string[] { "dolor" } })
                .ShouldBeNull();
        }

        [TestMethod]
        public void SamplerHighlightsKeyword()
        {
            var sampleGetter = new SampleGetter();

            sampleGetter.GetSample(GenerateStandardResultTestPage(),
                    new Query() { Fragments = new string[] { "dolor" } })
                .ShouldContain("<strong>dolor</strong>");
        }

        [TestMethod]
        public void SamplerHaxMaxLengthOfSample()
        {
            var sampleGetter = new SampleGetter();

            sampleGetter.GetSample(GenerateBigChunkResultTestPage(), 
                    new Query() { Fragments = new string[] { "dolor" } })
                .Length.ShouldBeLessThan(529);
        }

        [TestMethod]
        public void SamplerShowParagraphContainingKeyword()
        {
            var sampleGetter = new SampleGetter();

            sampleGetter.GetSample(GenerateStandardResultTestPage(), 
                    new Query() { Fragments = new string[] { "magna" } })
                .ShouldBe("Aliquam et fermentum leo. Curabitur odio nibh, convallis vel <strong>magna</strong> id, aliquam sagittis nisl. Pellentesque in purus fringilla purus tempor scelerisque. Nunc vestibulum euismod nisl, eu tincidunt sem posuere id. Vivamus non massa venenatis, ullamcorper lacus id, viverra elit. Aenean maximus eleifend augue, vel volutpat diam mattis sed. Morbi porttitor ipsum augue, sit amet sodales tellus pellentesque eu. Phasellus egestas quis est non lobortis. Nam ipsum nulla, mollis at sodales quis, consequat rutrum");
        }

        [TestMethod]
        public void HighlightKeepsCase()
        {
            var sampleGetter = new SampleGetter();

            sampleGetter.GetSample(GenerateStandardResultTestPage(), new Query() { Fragments = new string[] { "fusce" } })
                .ShouldBe("Sed finibus rhoncus diam quis ultrices. <strong>Fusce</strong> est est, suscipit ac nisl ut, auctor semper ligula. Curabitur aliquam ornare nisi ac finibus. Proin dui justo, tincidunt at dictum quis, pulvinar volutpat lacus. Praesent fringilla rutrum dui id volutpat. Quisque lacinia magna sit amet ligula viverra, nec molestie diam accumsan. In efficitur nisi felis, sed fermentum justo varius eu. Nulla fringilla a eros nec maximus.");
        }

        private Result GenerateStandardResultTestPage()
        {
            return GenerateResultTestPage(new string[] {
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue. Aenean aliquam feugiat suscipit. Nunc ultricies tortor vel augue viverra, sit amet tincidunt lectus faucibus. Aliquam vel aliquet purus. Nulla sed suscipit lorem. Nulla aliquam neque et massa dictum rhoncus non id enim. Suspendisse potenti. Cras dui ex, dapibus eu diam nec, convallis volutpat felis. Morbi convallis leo at diam placerat, vitae rutrum mauris dictum.",
                        "Aliquam et fermentum leo. Curabitur odio nibh, convallis vel magna id, aliquam sagittis nisl. Pellentesque in purus fringilla purus tempor scelerisque. Nunc vestibulum euismod nisl, eu tincidunt sem posuere id. Vivamus non massa venenatis, ullamcorper lacus id, viverra elit. Aenean maximus eleifend augue, vel volutpat diam mattis sed. Morbi porttitor ipsum augue, sit amet sodales tellus pellentesque eu. Phasellus egestas quis est non lobortis. Nam ipsum nulla, mollis at sodales quis, consequat rutrum metus. Curabitur consequat nulla a porttitor venenatis. Aliquam commodo mattis ligula id laoreet. Etiam ullamcorper tellus nec ullamcorper ultricies. Nam ac auctor mauris. Nunc vel tincidunt quam, at posuere erat. Ut id porta velit.",
                        "Sed finibus rhoncus diam quis ultrices. Fusce est est, suscipit ac nisl ut, auctor semper ligula. Curabitur aliquam ornare nisi ac finibus. Proin dui justo, tincidunt at dictum quis, pulvinar volutpat lacus. Praesent fringilla rutrum dui id volutpat. Quisque lacinia magna sit amet ligula viverra, nec molestie diam accumsan. In efficitur nisi felis, sed fermentum justo varius eu. Nulla fringilla a eros nec maximus.",
                        "In vulputate, quam placerat pellentesque hendrerit, felis massa blandit augue, vitae lobortis leo diam id libero. Cras porttitor metus vitae lorem faucibus, non vestibulum nulla placerat. Vivamus gravida mauris eu ante hendrerit imperdiet. Praesent malesuada non nunc pellentesque ultricies. Suspendisse finibus diam in purus vestibulum bibendum. Donec posuere diam sit amet fringilla condimentum. Nulla id sem sapien. Vestibulum sagittis ante quis quam lacinia interdum. Nulla faucibus nisl eget consectetur tristique. Donec eget arcu tortor. Nulla eu tellus ac urna ultricies aliquet a non metus. Aliquam lorem sapien, sollicitudin at augue id, ultrices posuere mi.",
                        "In vestibulum, orci ut tempus laoreet, massa eros scelerisque velit, ac elementum tellus urna ut mauris. Integer eu lobortis erat. Integer vitae ultricies lectus, vitae pulvinar nunc. Vestibulum varius volutpat ligula et accumsan. Etiam feugiat a libero eget porttitor. In non placerat tortor, a ultrices massa. Aliquam malesuada urna vitae libero lobortis tincidunt. Sed vel pulvinar ipsum, non vestibulum nunc. Proin sit amet enim nec sem placerat tempus in sed risus."
            });
        }

        private Result GenerateBigChunkResultTestPage()
        {
            return GenerateResultTestPage("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue. Aenean aliquam feugiat suscipit. Nunc ultricies tortor vel augue viverra, sit amet tincidunt lectus faucibus. Aliquam vel aliquet purus. Nulla sed suscipit lorem. Nulla aliquam neque et massa dictum rhoncus non id enim. Suspendisse potenti. Cras dui ex, dapibus eu diam nec, convallis volutpat felis. Morbi convallis leo at diam placerat, vitae rutrum mauris dictum."                        
                        + "Aliquam et fermentum leo. Curabitur odio nibh, convallis vel magna id, aliquam sagittis nisl. Pellentesque in purus fringilla purus tempor scelerisque. Nunc vestibulum euismod nisl, eu tincidunt sem posuere id. Vivamus non massa venenatis, ullamcorper lacus id, viverra elit. Aenean maximus eleifend augue, vel volutpat diam mattis sed. Morbi porttitor ipsum augue, sit amet sodales tellus pellentesque eu. Phasellus egestas quis est non lobortis. Nam ipsum nulla, mollis at sodales quis, consequat rutrum metus. Curabitur consequat nulla a porttitor venenatis. Aliquam commodo mattis ligula id laoreet. Etiam ullamcorper tellus nec ullamcorper ultricies. Nam ac auctor mauris. Nunc vel tincidunt quam, at posuere erat. Ut id porta velit."
                        + "Sed finibus rhoncus diam quis ultrices. Fusce est est, suscipit ac nisl ut, auctor semper ligula. Curabitur aliquam ornare nisi ac finibus. Proin dui justo, tincidunt at dictum quis, pulvinar volutpat lacus. Praesent fringilla rutrum dui id volutpat. Quisque lacinia magna sit amet ligula viverra, nec molestie diam accumsan. In efficitur nisi felis, sed fermentum justo varius eu. Nulla fringilla a eros nec maximus."
                        + "In vulputate, quam placerat pellentesque hendrerit, felis massa blandit augue, vitae lobortis leo diam id libero. Cras porttitor metus vitae lorem faucibus, non vestibulum nulla placerat. Vivamus gravida mauris eu ante hendrerit imperdiet. Praesent malesuada non nunc pellentesque ultricies. Suspendisse finibus diam in purus vestibulum bibendum. Donec posuere diam sit amet fringilla condimentum. Nulla id sem sapien. Vestibulum sagittis ante quis quam lacinia interdum. Nulla faucibus nisl eget consectetur tristique. Donec eget arcu tortor. Nulla eu tellus ac urna ultricies aliquet a non metus. Aliquam lorem sapien, sollicitudin at augue id, ultrices posuere mi."
                        + "In vestibulum, orci ut tempus laoreet, massa eros scelerisque velit, ac elementum tellus urna ut mauris. Integer eu lobortis erat. Integer vitae ultricies lectus, vitae pulvinar nunc. Vestibulum varius volutpat ligula et accumsan. Etiam feugiat a libero eget porttitor. In non placerat tortor, a ultrices massa. Aliquam malesuada urna vitae libero lobortis tincidunt. Sed vel pulvinar ipsum, non vestibulum nunc. Proin sit amet enim nec sem placerat tempus in sed risus."
            );
        }

        private Result GenerateResultTestPage(IEnumerable<string> content)
        {
            return new Result()
            {
                Content = content
            };
        }

        private Result GenerateResultTestPage(string content)
        {
            return GenerateResultTestPage(new string[] { content });
        }
    }
}
