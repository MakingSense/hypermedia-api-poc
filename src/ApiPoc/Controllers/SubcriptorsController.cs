using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;
using System.Xml.Serialization;

namespace ApiPoc.Controllers
{
    public class SubscriptorsController : Controller
    {
        [HttpGet("/accounts/{accountId}/subscriptors")]
        public IActionResult GetCollection(int accountId)
        {
            return new ObjectResult(new SubscriptorCollectionModel()
            {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<AccountsController>(x => x.GetItem(accountId)),
                    Url.LinkSubscriptorsDetailedCollection(accountId)
                },
                Items = new[]
                {
                    new SubscriptorModel() {
                        Links = new[] {
                            Url.LinkSelf<SubscriptorsController>(x => x.GetItem(accountId, 155))
                        },
                        FirstName = "Juan",
                        LastName = "Perez"
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}/subscriptors/detail")]
        public IActionResult GetDetailedCollection(int accountId)
        {
            return new ObjectResult(new SubscriptorCollectionModel()
            {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<AccountsController>(x => x.GetItem(accountId)),
                    Url.LinkSubscriptorsCollection(accountId)
                },
                Items = new[]
                {
                    new SubscriptorModel() {
                        Links = new[] {
                            Url.LinkSelf<SubscriptorsController>(x => x.GetItem(accountId, 155))
                        },
                        FirstName = "Juan",
                        LastName = "Perez",
                        Birday = DateTime.Parse("1980-01-01"),
                        Email = "juan@perez.com"
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}/subscriptors/{subscriptorId}")]
        public IActionResult GetItem(int accountId, int subscriptorId)
        {
            return new ObjectResult(new AccountModel()
            {
                Links = new[] {
                    Url.LinkSelf(),
                    Url.LinkParent<SubscriptorsController>(x => x.GetCollection(accountId))
                },
                FirstName = "Andrés",
                LastName = "Moschini",
                Email = "private@andresmoschini.com",
                Birday = DateTime.Parse("1978-12-02")
            });
        }
    }
}