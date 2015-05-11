using ApiPoc.Representations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.Helpers
{
    [Obsolete("Quick and dirty ugly patch")]
    public class LinkWithParent
    {
        public Link Link { get; private set; }
        public IRepresentation Parent { get; private set; }

        public LinkWithParent(Link link, IRepresentation parent)
        {
            Link = link;
            Parent = parent;
        }

        public T GetParentAs<T>()
            where T : class, IRepresentation
        {
            return Parent as T;
        }
    }
}
