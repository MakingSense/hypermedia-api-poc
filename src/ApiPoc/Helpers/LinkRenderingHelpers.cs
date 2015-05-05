using ApiPoc.Representations;
using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.Helpers
{
    public static class LinkRenderingHelpers
    {
        public static HtmlString Link(this IHtmlHelper html, LinkRepresentation link, string customText = null, string customClass = null)
        {
            if (link == null)
            {
                return HtmlString.Empty;
            }
            var bAnchor = new TagBuilder("a");
            bAnchor.Attributes.Add("rel", link.Rel);
            bAnchor.Attributes.Add("href", link.Href);
            if (customClass != null)
            {
                bAnchor.AddCssClass(customClass);
            }
            bAnchor.SetInnerText(customText ?? link.Description);
            return bAnchor.ToHtmlString(TagRenderMode.Normal);
        }

        public static HtmlString Links(this IHtmlHelper html, IEnumerable<LinkRepresentation> links, string customClass = null)
        {
            var bUl = new TagBuilder("ul");
            bUl.AddCssClass("links");
            if (customClass != null)
            {
                bUl.AddCssClass(customClass);
            }
            bUl.InnerHtml = string.Join("\n", links.Select(link =>
            {
                var bLi = new TagBuilder("li");
                bLi.InnerHtml = Link(html, link).ToString();
                return bLi.ToString(TagRenderMode.Normal);
            }));
            return bUl.ToHtmlString(TagRenderMode.Normal);
        }
    }
}
