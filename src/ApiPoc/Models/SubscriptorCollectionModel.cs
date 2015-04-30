using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ApiPoc.Controllers
{
    [XmlRoot("subscriptors", Namespace = "http://andresmoschini.github.io/hypermedia-api-poc")]
    public class SubscriptorCollectionModel : BaseModel
    {
        public SubscriptorModel[] Items { get; set; }

    }
}