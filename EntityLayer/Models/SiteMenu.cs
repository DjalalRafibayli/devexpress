using System;
using System.Collections.Generic;

namespace EntityLayer.Models
{
    public partial class SiteMenu
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? PermalinkAz { get; set; }
        public string? PermalinkRu { get; set; }
        public string? PermalinkEn { get; set; }
        public string? PermalinkTr { get; set; }
        public string? NameAz { get; set; }
        public string? NameRu { get; set; }
        public string? NameEn { get; set; }
        public string? NameTr { get; set; }
        public string? MenuIcon { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? Area { get; set; }
        public string? Link { get; set; }
        public string? TextAz { get; set; }
        public string? TextRu { get; set; }
        public string? TextEn { get; set; }
        public string? TextTr { get; set; }
        public string? DescriptionAz { get; set; }
        public string? DescriptionRu { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionTr { get; set; }
        public string? MetaDescriptionAz { get; set; }
        public string? MetaDescriptionRu { get; set; }
        public string? MetaDescriptionEn { get; set; }
        public string? MetaDescriptionTr { get; set; }
        public string? MetaKeywordAz { get; set; }
        public string? MetaKeywordRu { get; set; }
        public string? MetaKeywordEn { get; set; }
        public string? MetaKeywordTr { get; set; }
        public string? HtmlAz { get; set; }
        public string? HtmlRu { get; set; }
        public string? HtmlEn { get; set; }
        public string? HtmlTr { get; set; }
        public string? Icon { get; set; }
        public string? Img { get; set; }
        public string? SmallImg { get; set; }
        public byte? Position { get; set; }
        public DateTime? DateTime { get; set; }
        public int? OrderId { get; set; }
        public byte? Active { get; set; }
    }
}
