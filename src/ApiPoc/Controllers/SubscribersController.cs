using ApiPoc.Helpers;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using System;

namespace ApiPoc.Controllers
{
    public class SubscribersController : BaseController
    {
        private const int SAMPLE_ID = 155;

        [HttpGet("/accounts/{accountId}/subscribers")]
        public IActionResult Index(int accountId)
        {
            return Negotiated(new SubscriberCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriberCollection),
                    Url.Link<AccountsController>(x => x.Item(accountId), Rel.Parent | Rel.AccountCollection, "Account details"),
                    Url.Link<SubscribersController>(x => x.DetailedIndex(accountId), Rel.SubscriberCollection, "Subscribers list (detailed)"),
                },
                Items = new[]
                {
                    new SubscriberRepresentation() {
                        Id = SAMPLE_ID,
                        Links = new[] {
                            Url.Link<SubscribersController>(x => x.Item(accountId, SAMPLE_ID), Rel.Self | Rel.SubscriberItem, "Subscriber details")
                        },
                        FirstName = "Juan",
                        LastName = "Perez"
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}/subscribers/detail")]
        public IActionResult DetailedIndex(int accountId)
        {
            return Negotiated(new SubscriberCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriberDetailCollection),
                    Url.Link<AccountsController>(x => x.Item(accountId), Rel.Parent | Rel.AccountCollection, "Account details"),
                    Url.Link<SubscribersController>(x => x.Index(accountId), Rel.SubscriberCollection, "Subscribers list (simple)"),
                },
                Items = new[]
                {
                    new SubscriberRepresentation() {
                        Links = new[] {
                            Url.Link<SubscribersController>(x => x.Item(accountId, SAMPLE_ID), Rel.Self | Rel.SubscriberItem, "Subscriber details")
                        },
                        Id = SAMPLE_ID,
                        FirstName = "Juan",
                        LastName = "Perez",
                        Birthday = DateTime.Parse("1980-01-01"),
                        Email = "juan@perez.com"
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}/subscribers/{subscriberId}")]
        public IActionResult Item(int accountId, int subscriberId)
        {
            return Negotiated(new SubscriberRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriberItem),
                    Url.Link<SubscribersController>(x => x.Index(accountId), Rel.Parent | Rel.SubscriberCollection, "Subscribers list"),
                },
                Id = subscriberId,
                FirstName = "Andrés",
                LastName = "Moschini",
                Email = "private@andresmoschini.com",
                Birthday = DateTime.Parse("1978-12-02")
            });
        }
    }
}
