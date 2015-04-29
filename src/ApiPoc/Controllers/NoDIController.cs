using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;

namespace ApiPoc.Controllers
{
    public class NoDIController : Controller
    {
        [HttpGet("NoDI/{id}")]
        public NoDIModel GetItem(string id)
        {
            return new NoDIModel
            {
                Links = new[]
                {
                    new AtomLinkModel
                    {
                        Href = this.Url.Action(nameof(GetItem), new { id = id }),
                        Rel = "self"
                    },
                    new AtomLinkModel
                    {
                        Href = this.Url.Action("GetCollection", "Home"),
                        Rel = "http://sample.ploeh.dk/rels/home"
                    }
                },
                Name = id
            };
        }
    }
}