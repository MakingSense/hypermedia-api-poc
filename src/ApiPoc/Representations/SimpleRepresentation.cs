using ApiPoc.Helpers;

namespace ApiPoc.Representations
{
    public class SimpleRepresentation : IRepresentation
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
    }
}
