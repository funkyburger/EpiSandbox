using EpiSandbox.Data;
using EpiSandbox.Extensions;
using EpiSandbox.Models.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Linq;

namespace EpiSandbox.UnitTests.Extensions
{
    [TestClass]
    public class StringExtensionTests
    {
        //Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue. Aenean aliquam feugiat suscipit. Nunc ultricies tortor vel augue viverra, sit amet tincidunt lectus faucibus. Aliquam vel aliquet purus. Nulla sed suscipit lorem. Nulla aliquam neque et massa dictum rhoncus non id enim. Suspendisse potenti. Cras dui ex, dapibus eu diam nec, convallis volutpat felis. Morbi convallis leo at diam placerat, vitae rutrum mauris dictum.

        #region ContainsAllOrSomeWords
        [TestMethod]
        public void ContainsAllOrSomeWordsReturnsTrueIfOneWordIsFound()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit.".ContainsAllOrSomeWords("titi toto amet tata")
                .ShouldBeTrue();
        }

        [TestMethod]
        public void ContainsAllOrSomeWordsFalseIfNone()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit.".ContainsAllOrSomeWords("titi toto tutu tata")
                .ShouldBeFalse();
        }

        [TestMethod]
        public void ContainsAllOrSomeWordsIsNotCaseSensitive()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit.".ContainsAllOrSomeWords("lorem")
                .ShouldBeTrue();
        }
        #endregion

        #region HtmlBoldAllOrSomeWords
        [TestMethod]
        public void HtmlBoldAllOrSomeWordsBoldsLooseWords()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue."
                .HtmlBoldAllOrSomeWords("viverra nulla")
                .ShouldBe("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis <strong>viverra</strong> <strong>nulla</strong>. Duis <strong>viverra</strong>, orci et faucibus suscipit, <strong>nulla</strong> odio vestibulum orci, a tempor odio libero et augue.");
        }

        [TestMethod]
        public void HtmlBoldAllOrSomeWordsKeepsCase()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
                .HtmlBoldAllOrSomeWords("lorem")
                .ShouldBe("<strong>Lorem</strong> ipsum dolor sit amet, consectetur adipiscing elit.");
        }

        [TestMethod]
        public void HtmlBoldAllOrSomeWordsEndOfStringLimitCase()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. toto"
                .HtmlBoldAllOrSomeWords("toto")
                .ShouldBe("Lorem ipsum dolor sit amet, consectetur adipiscing elit. <strong>toto</strong>");
        }

        [TestMethod]
        public void HtmlBoldAllOrSomeWordsResetsPreviousBold()
        {
            "Lorem ipsum dolor sit amet, consectetur <strong>adipiscing elit</strong>. Morbi quis viverra nulla."
                .HtmlBoldAllOrSomeWords("amet")
                .ShouldBe("Lorem ipsum dolor sit <strong>amet</strong>, consectetur adipiscing elit. Morbi quis viverra nulla.");
        }
        #endregion

        [TestMethod]
        public void HtmlBoldWordsDoesItsJob()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue."
                .HtmlBoldWords(new string[] { "viverra nulla", "vestibulum orci" })
                .ShouldBe("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis <strong>viverra nulla</strong>. Duis viverra, orci et faucibus suscipit, nulla odio <strong>vestibulum orci</strong>, a tempor odio libero et augue.");
        }

        [TestMethod]
        public void HtmlBoldWordsResetsPreviousBold()
        {
            "Lorem ipsum dolor sit amet, <strong>consectetur adipiscing</strong> elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue."
                .HtmlBoldWords(new string[] { "viverra nulla", "vestibulum orci" })
                .ShouldBe("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis <strong>viverra nulla</strong>. Duis viverra, orci et faucibus suscipit, nulla odio <strong>vestibulum orci</strong>, a tempor odio libero et augue.");
        }

        [TestMethod]
        public void HtmlBoldWordsKeepsCase()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue."
                .HtmlBoldWords(new string[] { "Duis viverra", "vestibulum orci" })
                .ShouldBe("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. <strong>Duis viverra</strong>, orci et faucibus suscipit, nulla odio <strong>vestibulum orci</strong>, a tempor odio libero et augue.");
        }

        [TestMethod]
        public void HtmlBoldWordsIsCaseInsensitive()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue."
                .HtmlBoldWords(new string[] { "lorem ipsum" })
                .ShouldBe("<strong>Lorem ipsum</strong> dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue.");
        }

        [TestMethod]
        public void HtmlBoldWordsSkipsNonWords()
        {
            "Lorem ipsum dolor sit,   amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue."
                .HtmlBoldWords(new string[] { "sit amet" })
                .ShouldBe("Lorem ipsum dolor <strong>sit,   amet</strong>, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue.");
        }

        #region IndexesOfAll
        [TestMethod]
        public void CapLengthCutsOffBetweenTwoWords()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla."
                .CapLength(60)
                .ShouldBe("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
        }

        [TestMethod]
        public void IndexesOfAllReturnsIndexes()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue."
                .IndexesOfAll("nulla")
                .ToArray()
                .ShouldBe(new int[] { 76, 124 });
        }

        [TestMethod]
        public void IndexesOfAllReturnsIndexesRegardlessOfCase()
        {
            "Lorem Ipsum dolor sit amet, consectetur adipiscing elit."
                .IndexesOfAll("ipsum")
                .ToArray()
                .ShouldBe(new int[] { 6 });
        }

        [TestMethod]
        public void IndexesOfAllReturnsIndexesLimitCaseEndOfStringAndMinimalLength()
        {
            "a b c d e"
                .IndexesOfAll("e")
                .ToArray()
                .ShouldBe(new int[] { 8 });
        }

        [TestMethod]
        public void IndexesOfAllReturnsEmptyIfNoneIsThere()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi quis viverra nulla. Duis viverra, orci et faucibus suscipit, nulla odio vestibulum orci, a tempor odio libero et augue."
                .IndexesOfAll("pouet")
                .ToArray()
                .ShouldBe(new int[] { });
        }
        #endregion
    }
}
