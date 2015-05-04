using ApiPoc.Controllers;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ApiPoc.Helpers
{
    public class LinkBag
    {
        public LinkRepresentation[] Links { get; private set; }
        ILookup<string, LinkRepresentation> linksByRel;
        HashSet<LinkRepresentation> alreadyUsed;

        public LinkBag(LinkRepresentation[] links)
        {
            Links = links ?? new LinkRepresentation[] { };
            linksByRel = Links
                .SelectMany(link => link.Rel.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(rel => new { rel, link }))
                .ToLookup(x => x.rel, x => x.link);
            alreadyUsed = new HashSet<LinkRepresentation>();
        }

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

        public LinkRepresentation[] GetUnusedLinks()
        {
            return Links.Where(x => !alreadyUsed.Contains(x)).ToArray();
        }

        public void Reset()
        {
            alreadyUsed.Clear();
        }
    }
}