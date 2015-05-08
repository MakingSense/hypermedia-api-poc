using System;
using System.Collections.Generic;
using System.Linq;
using ApiPoc.Helpers;

namespace ApiPoc.Representations
{
    public abstract class CollectionRepresentation<T> : BaseRepresentation
    {
        public T[] Items { get; set; } = new T[] { };

        public int? PageSize { get; set; }

        public int? CurrentPage { get; set; }

        public int? ItemsCount { get; set; }

        public int? PagesCount { get { return PageSize.HasValue ? (ItemsCount + PageSize - 1) / PageSize : null; } }

        public CollectionRepresentation()
        {

        }

        public CollectionRepresentation(IEnumerable<T> allItems, int pageSize, int page, Func<int?, Rel, string, Link> linkGenerator, params Link[] moreLinks)
        {
            Items = allItems.Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            PageSize = pageSize;
            CurrentPage = page;
            ItemsCount = allItems.Count();

            var links = new List<Link>();
            if (PagesCount > 1)
            {
                if (CurrentPage > 1)
                {
                    links.Add(linkGenerator(null, Rel.FirstPage, "First Page"));
                    links.Add(linkGenerator(CurrentPage - 1, Rel.PreviousPage, "Previous Page"));
                }
                if (CurrentPage < PagesCount)
                {
                    links.Add(linkGenerator(CurrentPage + 1, Rel.NextPage, "Next Page"));
                    links.Add(linkGenerator(PagesCount, Rel.LastPage, "Last Page"));
                }

                //TODO: fix this ugly patch because TemplateParameter does not work here
                //var specificPageLink = linkGenerator(TemplateParameter.Create<int?>("page"), Rel.SpecificPage, "Specific Page");
                var specificPageLink = linkGenerator(0, Rel.SpecificPage, "Specific Page");
                specificPageLink.Href = specificPageLink.Href.Replace("page=0", "page={page}");
                links.Add(specificPageLink);
            }
            links.AddRange(moreLinks);

            Links = links.ToArray();
        }

        protected int GetCollectionHash()
        {
            var hash = GetLinkBag().GetHashCode();
            unchecked // Overflow is fine, just wrap
            {
                hash = hash * 23 + (PageSize == null ? 587 : PageSize.GetHashCode());
                hash = hash * 23 + (CurrentPage == null ? 587 : CurrentPage.GetHashCode());
                hash = hash * 23 + (ItemsCount == null ? 587 : ItemsCount.GetHashCode());
            }
            return hash;        
        }
    }
}
