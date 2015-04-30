using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;

namespace ApiPoc.Controllers
{
    public class HomeController : Controller
    {
        private const int CURRENT_ACCOUNT_ID = 128;

        [HttpGet("/")]
        public IActionResult GetRoot()
        {
            return new ObjectResult(new HomeModel() {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkAccountsRoot(),
                    Url.LinkAccountHome(CURRENT_ACCOUNT_ID)
                }
            });
        }

    }
}