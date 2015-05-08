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

        public virtual string GetEtag()
        {
            // Etag disabled
            return null;

            // Maybe it is convenient to generate a value hash using reflection
            // fre0n proposes an implementation: http://stackoverflow.com/questions/5569545/how-to-generate-a-unique-hash-code-for-an-object-based-on-its-contents#answer-5569869
            // but I am not sure if it works for arrays.
        }
    }
}
