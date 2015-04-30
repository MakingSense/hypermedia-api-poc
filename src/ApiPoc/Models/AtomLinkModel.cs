using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNet.Mvc;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiPoc.Controllers
{
    [XmlType("link", Namespace = "http://www.w3.org/2005/Atom")]
    public class AtomLinkModel
    {
        [XmlAttribute("href")]
        public string Href { get; set; }

        [XmlAttribute("rel")]
        public string Rel { get; set; }
        
        public static AtomLinkModel AccountHome(string href)
        {
            return new AtomLinkModel() { Href = href, Rel = "ClientHome" };
        }

    }
   
}