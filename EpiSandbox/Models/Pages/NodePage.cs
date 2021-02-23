using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using System;
using System.ComponentModel.DataAnnotations;

namespace EpiSandbox.Models.Pages
{
    [ContentType(DisplayName = "NodePage", GUID = "d2f6d522-c35c-43a0-9142-112d1b436541", Description = "")]
    public class NodePage : StandardContentPage
    {
    }
}