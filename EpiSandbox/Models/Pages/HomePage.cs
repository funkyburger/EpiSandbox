using EpiSandbox.Data;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EpiSandbox.Models.Pages
{
    [ContentType(DisplayName = "HomePage", GUID = "2ca92563-14ab-4c4c-b5ca-f757259a22e4", Description = "")]
    public class HomePage : StandardContentPage, IContentPage
    {
        public IEnumerable<string> Content => new string[] { MainBody.ToHtmlString() };
    }
}