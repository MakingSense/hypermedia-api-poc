using ApiPoc.Representations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.Helpers
{
    public class LinkWithParent<T> where T : IRepresentation
    {
        public Link Link { get; private set; }
        public T Parent { get; private set; }

        public LinkWithParent(Link link, T parent)
        {
            Link = link;
            Parent = parent;
        }
    }
}
