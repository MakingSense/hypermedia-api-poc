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
            return new ObjectResult(new AccountCollectionModel() {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<HomeController>(x => x.GetRoot()),
                    Url.LinkAccountResource(CURRENT_ACCOUNT_ID),
                    Url.LinkAccountDetailedCollection()
                },
                Items = new []
                {
                    new AccountModel() {
                        Links = new[] {
                            Url.LinkSelf<AccountsController>(x => x.GetItem(0), new { accountId = CURRENT_ACCOUNT_ID })
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
            return new ObjectResult(new AccountCollectionModel()
            {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<HomeController>(x => x.GetRoot()),
                    Url.LinkAccountResource(CURRENT_ACCOUNT_ID),
                    Url.LinkAccountCollection()
                },
                Items = new[]
                {
                    new AccountModel() {
                        Links = new[] {
                            Url.LinkSelf<AccountsController>(x => x.GetItem(0), new { accountId = CURRENT_ACCOUNT_ID })
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
            return new ObjectResult(new AccountModel() {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<AccountsController>(x => x.GetCollection())
                },
                FirstName = "Andrés",
                LastName = "Moschini",
                Email = "private@andresmoschini.com",
                Birday = DateTime.Parse("1978-12-02")
            });
        }
    }
}