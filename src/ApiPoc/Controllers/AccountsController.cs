using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;
using System.Xml.Serialization;
using ApiPoc.Representations;
using ApiPoc.Helpers;

namespace ApiPoc.Controllers
{
    public class AccountsController : Controller
    {
        private const int CURRENT_ACCOUNT_ID = 128;

        [HttpGet("/accounts")]
        public IActionResult GetCollection()
        {

            return new ObjectResult(new AccountCollectionRepresentation() {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.AccountCollection),
                    Url.Link<HomeController>(x => x.GetRoot(), Rel.Parent | Rel.Home, "Home"),
                    Url.Link<AccountsController>(x => x.GetItem(CURRENT_ACCOUNT_ID), Rel.AccountItem, "My account details")
                },
                Items = new []
                {
                    new AccountRepresentation() {
                        Links = new[] {
                            Url.Link<AccountsController>(x => x.GetItem(CURRENT_ACCOUNT_ID), Rel.Self | Rel.AccountItem, "Account details")
                        },
                        FirstName = "Andrés",
                        LastName = "Moschini"
                    }
                }
            });   
        }

        [HttpGet("/accounts/{accountId}")]
        public IActionResult GetItem(int accountId)
        {
            return new ObjectResult(new AccountRepresentation() {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.AccountItem),
                    Url.Link<AccountsController>(x => x.GetCollection(), Rel.Parent | Rel.AccountItem, "Accounts list"),
                    Url.Link<SubscriptorsController>(x => x.GetCollection(accountId), Rel.SubscriptorCollection, "Subscription list"),
                },
                FirstName = "Andrés",
                LastName = "Moschini",
                Email = "private@andresmoschini.com",
                Birday = DateTime.Parse("1978-12-02")
            });
        }
    }
}