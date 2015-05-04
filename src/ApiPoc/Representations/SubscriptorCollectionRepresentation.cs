using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ApiPoc.Representations
{
    public class SubscriptorCollectionRepresentation : BaseRepresentation
    {
        public SubscriptorRepresentation[] Items { get; set; }

    }
}