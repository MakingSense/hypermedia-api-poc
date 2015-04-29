using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;

namespace ApiPoc.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HomeModelCollection GetCollection()
        {
            return new HomeModelCollection
            {
                Links = new[]
                {
                    new AtomLinkModel
                    {
                        Href = this.Url.Action(nameof(GetCollection)),
                        Rel = "self"
                    }
                },
                Homes = new[]
                {
                    new HomeModel
                    {
                        Name = "ploeh",
                        Links = new[]
                        {
                            new AtomLinkModel
                            {
                                Href = this.Url.Action(nameof(GetItem), new { id = "ploe" }),
                                Rel = "http://sample.ploeh.dk/rels/specific-home"
                            },
                            new AtomLinkModel
                            {
                                Href = this.Url.Action("GetItem", "NoDI", new { id = "ploe" }),
                                Rel = "http://sample.ploeh.dk/rels/no-di"
                            }
                        }
                    },
                    new HomeModel
                    {
                        Name = "fnaah",
                        Links = new[]
                        {
                            new AtomLinkModel
                            {
                                Href = this.Url.Action(nameof(GetItem), new { id = "fnaah" }),
                                Rel = "http://sample.ploeh.dk/rels/specific-home"
                            },
                            new AtomLinkModel
                            {
                                Href = this.Url.Action("GetItem", "NoDI", new { id = "fnaah" }),
                                Rel = "http://sample.ploeh.dk/rels/no-di"
                            }
                        }
                    }
                }
            };
        }

        [HttpGet("custom/route/{id}")]
        public HomeModel GetItem(string id)
        {
            return new HomeModel
            {
                Name = id,
                Links = new[]
                {
                    new AtomLinkModel
                    {
                        Href = this.Url.Action(nameof(GetItem), new { id = id }),
                        Rel = "self"
                    },
                    new AtomLinkModel
                    {
                        Href = this.Url.Action(nameof(GetCollection)),
                        Rel = "http://sample.ploeh.dk/rels/home"
                    },
                    new AtomLinkModel
                    {
                        Href = this.Url.Action(nameof(NoDIController.GetItem), nameof(NoDIController), new { id = id }),
                        Rel = "http://sample.ploeh.dk/rels/no-di"
                    }
                }
            };
        }
    }
}