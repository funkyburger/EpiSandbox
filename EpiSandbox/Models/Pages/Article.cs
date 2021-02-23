using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiSandbox.Models.Pages
{
    [ContentType(DisplayName = "Article", GUID = "5d5d8a95-9060-4069-abcf-ec26df60a6cd", Description = "")]
    public class Article : StandardContentPage
    {
    }
}