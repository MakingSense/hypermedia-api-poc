using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ApiPoc.Controllers
{
    [XmlRoot("home", Namespace = "http://www.ploeh.dk/hyprlinkr/sample/2012")]
    public abstract class BaseModel
    {
        [XmlArray("links")]
        [XmlArrayItem("link", Namespace = "http://www.w3.org/2005/Atom")]
        public AtomLinkModel[] Links { get; set; }
    }
}