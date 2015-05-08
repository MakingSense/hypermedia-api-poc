using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.Helpers
{
    public class ActionForm : MvcForm
    {
        private ViewContext _viewContext;

        public ActionForm(ViewContext viewContext) 
            : base(viewContext)
        {
            _viewContext = viewContext;
        }

        protected override void GenerateEndForm()
        {
            this._viewContext.Writer.Write(@"
<input name=""generate-request"" class=""generate -query ignore-on-send"" type=""button"" value=""Generate request"" />
<div>
    <textarea name=""generated-request"" class=""ignore-on-send"" style=""display:none"" cols=""80"" rows=""10"" readonly=""readonly""></textarea>
</div>
<input name=""submit-request"" class=""submit-query ignore-on-send"" type=""button"" value=""Submit request"" />
<div>
    <textarea name=""server-response"" class=""ignore-on-send"" style=""display:none"" cols=""80"" rows=""10"" readonly=""readonly""></textarea>
</div>
");
            base.GenerateEndForm();
        }
    }

    public static class LinkRenderingHelpers
    {
        public static HtmlString EmptyForm(this IHtmlHelper html, Link link, string customText = null, string customClass = null)
        {
            using (html.BeginForm(link, customText, customClass))
            {
                return HtmlString.Empty;
            }
        }

        public static ActionForm BeginForm(this IHtmlHelper html, Link link, string customText = null, string customClass = null)
        {
            html.ViewContext.Writer.Write(html.Link(link, customText, customClass));
            var bForm = new TagBuilder("form");
            if (customClass != null)
            {
                bForm.AddCssClass(customClass);
            }
            
            //TODO: Add if-match or other attributes to support optimistic concurrency

            bForm.Attributes.Add("action", link.Href);
            bForm.Attributes.Add("data-method",
                (link.RawRel & Rel._Delete) == Rel._Delete ? "DELETE"
                : (link.RawRel & Rel._Put) == Rel._Put ? "PUT"
                : (link.RawRel & Rel._Post) == Rel._Post ? "POST"
                : "GET");
            bForm.Attributes.Add("style", "display: none");
            html.ViewContext.Writer.Write(bForm.ToHtmlString(TagRenderMode.StartTag));

            //TODO: render an input for each template item
            //html.ViewContext.Writer.Write(...

            return new ActionForm(html.ViewContext);
        }

        public static HtmlString Link(this IHtmlHelper html, Link link, string customText = null, string customClass = null)
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

        public static HtmlString Links(this IHtmlHelper html, IEnumerable<Link> links, string customClass = null)
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
