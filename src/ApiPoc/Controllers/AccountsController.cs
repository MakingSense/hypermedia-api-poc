using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;
using System.Xml.Serialization;

namespace ApiPoc.Controllers
{
    public class AccountsController : Controller
    {
        private const int CURRENT_ACCOUNT_ID = 128;

        [HttpGet("/accounts")]
        public IActionResult GetCollection()
        {
            return new ObjectResult(new AccountModel() {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<HomeController>(x => x.GetRoot()),
                    Url.LinkAccountHome(CURRENT_ACCOUNT_ID)
                }
            });
        }

        [HttpGet("/accounts/{accountId}")]
        public IActionResult GetItem(int accountId)
        {
            return new ObjectResult(new AccountCollectionModel() {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<AccountsController>(x => x.GetCollection())
                }
            });
        }
    }
}