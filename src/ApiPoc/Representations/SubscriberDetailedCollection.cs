using System;
using System.Collections.Generic;

namespace ApiPoc.Representations
{
    public class SubscriberDetailedCollection : CollectionRepresentation<SubscriberDetail>
    {
        public SubscriberDetailedCollection()
            :base()
        {

        }

        public SubscriberDetailedCollection(IEnumerable<SubscriberDetail> allItems, int pageSize, int page, Func<int?, Rel, string, Link> linkGenerator, params Link[] moreLinks)
            : base(allItems, pageSize, page, linkGenerator, moreLinks)
        {
        }

        public override string GetEtag()
        {
            return $"W/\"{ GetCollectionHash().ToString() }\"";
        }
    }
}
