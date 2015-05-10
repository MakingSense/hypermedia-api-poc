using ApiPoc.Representations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.Helpers
{
    public class LinkBag
    {
        private HashSet<Link> alreadyUsed;

        private ILookup<string, Link> linksByRel;

        public LinkBag(Link[] links)
        {
            Links = links ?? new Link[] { };
            linksByRel = Links
                .SelectMany(link => link.Rel.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(rel => new { rel, link }))
                .ToLookup(x => x.rel, x => x.link);
            alreadyUsed = new HashSet<Link>();
        }

        public Link[] Links { get; private set; }

        public Link[] GetByRel(Rel rel)
        {
            return GetByRel(rel.ToRelString());
        }

        public Link[] GetByRel(string rel)
        {
            var result = (linksByRel[rel] ?? Enumerable.Empty<Link>()).ToArray();
            foreach (var item in result)
            {
                alreadyUsed.Add(item);
            }
            return result;
        }

        public Link GetFirstByRel(Rel rel)
        {
            return GetFirstByRel(rel.ToRelString());
        }

        public Link GetFirstByRel(string rel)
        {
            var result = (linksByRel[rel] ?? Enumerable.Empty<Link>()).FirstOrDefault();
            if (result != null)
            {
                alreadyUsed.Add(result);
            }
            return result;
        }

        public Link[] GetUnusedLinks(bool? plain = null)
        {
            var links = Links.Where(x => !alreadyUsed.Contains(x));
            if (plain.HasValue)
            {
                links = links.Where(x => (x.RawRel & Rel._Unsafe) == 0 && (x.RawRel & Rel._Template) == 0);
            }
            return links.ToArray();
        }

        public Link[] GetUnusedPlainLinks()
        {
            return GetUnusedLinks(true);
        }

        public Link[] GetUnusedUnsafeLinks()
        {
            return GetUnusedLinks(false);
        }

        public void Reset()
        {
            alreadyUsed.Clear();
        }
    }
}
