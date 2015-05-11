using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                link.Rel.Is(Rel._Delete) ? "DELETE"
                : link.Rel.Is(Rel._Put) ? "PUT"
                : link.Rel.Is(Rel._Post) ? "POST"
                : "GET");
            bForm.Attributes.Add("style", "display: none");
            html.ViewContext.Writer.Write(bForm.ToHtmlString(TagRenderMode.StartTag));

            //read from settings
            var documentationBaseUrl = "https://github.com/andresmoschini/hypermedia-api-poc/blob/master/src/ApiPoc/wwwroot/docs/";
            var relList = link.Rel.ToRelString().Split(' ');
            foreach (var relItem in relList)
            {
                html.ViewContext.Writer.Write("<small><a target=\"_blank\" href =\"" + documentationBaseUrl + "rels/" + relItem + ".md\")\">See documentation about <code>" + relItem + "</code> relation</a></small><br />");
            }

            if (link.Rel.Is(Rel._Template))
            {
                html.ViewContext.Writer.Write("<fieldset><legend>URI parameters</legend>");

                var templateKeys = new Regex("{([^{}]*)")
                    .Matches(link.Href).Cast<Match>()
                    .Where(x => x.Length > 1)
                    .Select(x => x.Value.Substring(1));

                foreach (var templateKey in templateKeys)
                {
                    var bDiv = new TagBuilder("div");

                    var bLabel = new TagBuilder("label");
                    bLabel.Attributes.Add("for", templateKey);
                    bLabel.SetInnerText(templateKey);

                    bDiv.InnerHtml += bLabel.ToString(TagRenderMode.Normal);

                    var bInput = new TagBuilder("input");
                    bInput.Attributes.Add("name", templateKey);
                    bInput.AddCssClass("uri-template");

                    bDiv.InnerHtml += " " + bInput.ToString(TagRenderMode.SelfClosing);
                    html.ViewContext.Writer.Write(bDiv.ToString(TagRenderMode.Normal));
                }

                html.ViewContext.Writer.Write("</fieldset>");
                
            }

            return new ActionForm(html.ViewContext);
        }

        public static HtmlString Link(this IHtmlHelper html, Link link, string customText = null, string customClass = null)
        {
            if (link == null)
            {
                return HtmlString.Empty;
            }
            var bAnchor = new TagBuilder("a");
            bAnchor.Attributes.Add("rel", link.Rel.ToRelString());
            bAnchor.Attributes.Add("href", link.Href);
            if (customClass != null)
            {
                bAnchor.AddCssClass(customClass);
            }
            bAnchor.SetInnerText(customText ?? link.Description);
            return bAnchor.ToHtmlString(TagRenderMode.Normal);
        }
    }
}
