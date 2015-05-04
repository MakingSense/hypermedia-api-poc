using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;
using ApiPoc.Helpers;

namespace ApiPoc.Controllers
{
    public class HomeController : Controller
    {
        private const int CURRENT_ACCOUNT_ID = 128;

        [HttpGet("/")]
        public IActionResult GetRoot()
        {
            return new ObjectResult(new HomeRepresentation() {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkAccountCollection(),
                    Url.LinkAccountResource(CURRENT_ACCOUNT_ID)
                }
            });
        }

    }
}