using ApiPoc.Helpers;
using System.Collections;
using System.Collections.Generic;

namespace ApiPoc.Representations
{
    public abstract class BaseRepresentation : IRepresentation
    {
        private LinkBag linkBag;

        private Link[] links;

        public Link[] Links
        {
            get { return links; }
            set
            {
                links = value;
                linkBag = null;
            }
        }

        public LinkBag GetLinkBag()
        {
            if (linkBag == null)
            {
                linkBag = new LinkBag(links);
            }
            return linkBag;
        }

        public int? CustomStatusCode { get; set; }
    }
}
