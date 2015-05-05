﻿using ApiPoc.Helpers;

namespace ApiPoc.Representations
{
    public abstract class BaseRepresentation
    {
        private LinkBag linkBag;

        private LinkRepresentation[] links;

        public LinkRepresentation[] Links
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
    }
}
