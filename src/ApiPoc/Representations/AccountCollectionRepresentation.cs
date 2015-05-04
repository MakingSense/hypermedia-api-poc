using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ApiPoc.Representations
{
    public class AccountCollectionRepresentation : BaseRepresentation
    {
        public AccountRepresentation[] Items { get; set; }

    }
}