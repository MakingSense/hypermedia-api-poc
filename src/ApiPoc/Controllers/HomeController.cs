using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;
using ApiPoc.Helpers;
using ApiPoc.Representations;

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
                    Url.LinkSelf(Rel.Home),
                    Url.Link<AccountsController>(x => x.GetCollection(), Rel.AccountCollection, "Account List"),
                    Url.Link<AccountsController>(x => x.GetItem(CURRENT_ACCOUNT_ID), Rel.AccountItem, "My account details")
                }
            });
        }

    }
}