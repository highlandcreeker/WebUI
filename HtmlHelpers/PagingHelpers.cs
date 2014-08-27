using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

using FBPortal.WebUI.Models;

namespace FBPortal.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        private static int maxPagesToShow = 10;
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            int firstPage = System.Math.Max(1, (pagingInfo.CurrentPage - 5));
            int lastPage = System.Math.Min(pagingInfo.TotalPages, (firstPage + maxPagesToShow));

            TagBuilder firstTag = new TagBuilder("a");
            firstTag.AddCssClass("fb360-a");
            firstTag.MergeAttribute("href", pageUrl(1));
            firstTag.InnerHtml = "1";

            result.Append(firstTag.ToString());

            TagBuilder barTag = new TagBuilder("span");
            barTag.Attributes.Add("style", "margin: 2px 2px 2px 2px;");
            barTag.InnerHtml = "[";

            result.Append(barTag);

            for (int i = firstPage; i <= lastPage; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.AddCssClass("fb360-a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("fb360-a-selected");

                result.Append(tag.ToString());
            }

             TagBuilder lastTag = new TagBuilder("a");
             lastTag.AddCssClass("fb360-a");
             lastTag.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
             lastTag.InnerHtml = pagingInfo.TotalPages.ToString();

             barTag.InnerHtml = "]";
             result.Append(barTag);
             result.Append(lastTag.ToString());


            return MvcHtmlString.Create(result.ToString());
        }
    }
}