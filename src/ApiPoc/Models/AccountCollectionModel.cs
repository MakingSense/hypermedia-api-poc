using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ApiPoc.Controllers
{
    [XmlRoot("accounts", Namespace = "http://andresmoschini.github.io/hypermedia-api-poc")]
    public class AccountCollectionModel : BaseModel
    {
        public AccountModel[] Items { get; set; }

    }
}