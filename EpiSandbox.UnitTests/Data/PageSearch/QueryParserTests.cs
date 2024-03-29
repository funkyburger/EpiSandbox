﻿using EpiSandbox.Data;
using EpiSandbox.Data.PageSearch;
using EpiSandbox.Models.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Linq;

namespace EpiSandbox.UnitTests.Data.PageSearch
{
    [TestClass]
    public class QueryParserTests
    {
        [TestMethod]
        public void ParserParsesLooseWords()
        {
            var parser = new QueryParser();

            var query = parser.Parse("toto titi tutu");
            query.Fragments.ToArray().ShouldBe(new string[] { "toto", "titi", "tutu" });
        }

        [TestMethod]
        public void ParserParsesGroupedWords()
        {
            var parser = new QueryParser();

            var query = parser.Parse("\"toto titi\"");
            query.Fragments.ToArray().ShouldBe(new string[] { "toto titi" });
        }

        [TestMethod]
        public void ParserParsesLooseAndGroupedWords()
        {
            var parser = new QueryParser();

            var query = parser.Parse("\"toto titi\" tutu bidule \"fraises tagada\"");
            query.Fragments.ToArray().ShouldBe(new string[] { "toto titi", "tutu", "bidule", "fraises tagada" });
        }

        [TestMethod]
        public void SingleQuoteDoesntGroup()
        {
            var parser = new QueryParser();

            var query = parser.Parse("toto titi \"tutu tata");
            query.Fragments.ToArray().ShouldBe(new string[] { "toto", "titi", "tutu", "tata" });
        }

        [TestMethod]
        public void EmptyQuotesAreIgnored()
        {
            var parser = new QueryParser();

            var query = parser.Parse("toto \"   \" titi");
            query.Fragments.ToArray().ShouldBe(new string[] { "toto", "titi" });
        }

        [TestMethod]
        public void TrailingBlankspacesAreIgnored()
        {
            var parser = new QueryParser();

            var query = parser.Parse("toto     titi");
            query.Fragments.ToArray().ShouldBe(new string[] { "toto", "titi" });
        }

        [TestMethod]
        public void QueriesAreDeparsed()
        {
            var parser = new QueryParser();
            var query = new Query()
            {
                Fragments = new string[] { "lorem ipsum", "bla bla bla", "bidule truc muche" }
            };

            parser.Deparse(query).ShouldBe("\"lorem ipsum\" \"bla bla bla\" \"bidule truc muche\"");
        }
    }
}
