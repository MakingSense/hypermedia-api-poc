using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ApiPoc.Controllers
{
    [XmlRoot("home", Namespace = "http://andresmoschini.github.io/hypermedia-api-poc")]
    public class HomeModel : BaseModel
    {

    }
}