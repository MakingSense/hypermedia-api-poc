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
                    Url.LinkSelf(),
                    Url.LinkParent<HomeController>(x => x.GetRoot()),
                    Url.LinkAccountResource(CURRENT_ACCOUNT_ID),
                    Url.LinkAccountDetailedCollection()
                },
                Items = new []
                {
                    new AccountRepresentation() {
                        Links = new[] {
                            Url.LinkSelf<AccountsController>(x => x.GetItem(CURRENT_ACCOUNT_ID))
                        },
                        FirstName = "Andrés",
                        LastName = "Moschini"
                    }
                }
            });   
        }

        [HttpGet("/accounts/detail")]
        public IActionResult GetDetailedCollection()
        {
            return new ObjectResult(new AccountCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<HomeController>(x => x.GetRoot()),
                    Url.LinkAccountResource(CURRENT_ACCOUNT_ID),
                    Url.LinkAccountCollection()
                },
                Items = new[]
                {
                    new AccountRepresentation() {
                        Links = new[] {
                            Url.LinkSelf<AccountsController>(x => x.GetItem(CURRENT_ACCOUNT_ID))
                        },
                        FirstName = "Andrés",
                        LastName = "Moschini",
                        Email = "private@andresmoschini.com",
                        Birday = DateTime.Parse("1978-12-02")
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}")]
        public IActionResult GetItem(int accountId)
        {
            return new ObjectResult(new AccountRepresentation() {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<AccountsController>(x => x.GetCollection()),
                    Url.LinkSubscriptorsCollection(accountId)
                },
                FirstName = "Andrés",
                LastName = "Moschini",
                Email = "private@andresmoschini.com",
                Birday = DateTime.Parse("1978-12-02")
            });
        }
    }
}