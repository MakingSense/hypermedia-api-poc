using System;
using System.Collections.Generic;

namespace ApiPoc.Representations
{
    public class SubscriberCollection : CollectionRepresentation<SubscriberCollectionItem>
    {
        public SubscriberCollection()
            :base()
        {

        }

        public SubscriberCollection(IEnumerable<SubscriberCollectionItem> allItems, int pageSize, int page, Func<int?, Rel, string, Link> linkGenerator, params Link[] moreLinks)
            : base(allItems, pageSize, page, linkGenerator, moreLinks)
        {
        }
    }
}
