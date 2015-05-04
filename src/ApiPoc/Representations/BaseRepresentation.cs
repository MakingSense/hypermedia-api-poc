using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ApiPoc.Representations
{
    public abstract class BaseRepresentation
    {
        public LinkRepresentation[] Links { get; set; }
    }
}