using ApiPoc.Representations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.Helpers
{
    public class LinkBag
    {
        private HashSet<LinkRepresentation> alreadyUsed;

        private ILookup<string, LinkRepresentation> linksByRel;

        public LinkBag(LinkRepresentation[] links)
        {
            Links = links ?? new LinkRepresentation[] { };
            linksByRel = Links
                .SelectMany(link => link.Rel.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(rel => new { rel, link }))
                .ToLookup(x => x.rel, x => x.link);
            alreadyUsed = new HashSet<LinkRepresentation>();
        }

        public LinkRepresentation[] Links { get; private set; }

        public LinkRepresentation[] GetByRel(Rel rel)
        {
            return GetByRel(rel.ToRelString());
        }

        public LinkRepresentation[] GetByRel(string rel)
        {
            var result = (linksByRel[rel] ?? Enumerable.Empty<LinkRepresentation>()).ToArray();
            foreach (var item in result)
            {
                alreadyUsed.Add(item);
            }
            return result;
        }

        public LinkRepresentation GetFirstByRel(Rel rel)
        {
            return GetFirstByRel(rel.ToRelString());
        }

        public LinkRepresentation GetFirstByRel(string rel)
        {
            var result = (linksByRel[rel] ?? Enumerable.Empty<LinkRepresentation>()).FirstOrDefault();
            if (result != null)
            {
                alreadyUsed.Add(result);
            }
            return result;
        }

        public LinkRepresentation[] GetUnusedLinks(bool? safe = null)
        {
            var links = Links.Where(x => !alreadyUsed.Contains(x));
            if (safe.HasValue)
            {
                links = links.Where(x => x.Safe == safe);
            }
            return links.ToArray();
        }

        public LinkRepresentation[] GetUnusedSafeLinks()
        {
            return GetUnusedLinks(true);
        }

        public LinkRepresentation[] GetUnusedUnsafeLinks()
        {
            return GetUnusedLinks(false);
        }

        public void Reset()
        {
            alreadyUsed.Clear();
        }
    }
}
