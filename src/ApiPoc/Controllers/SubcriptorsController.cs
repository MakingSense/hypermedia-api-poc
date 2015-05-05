using ApiPoc.Helpers;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using System;

namespace ApiPoc.Controllers
{
    public class SubscriptorsController : BaseController
    {
        private const int SAMPLE_ID = 155;

        [HttpGet("/accounts/{accountId}/subscriptors")]
        public IActionResult Index(int accountId)
        {
            return new ObjectResult(new SubscriptorCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriptorCollection),
                    Url.Link<AccountsController>(x => x.Item(accountId), Rel.Parent | Rel.AccountCollection, "Account details"),
                    Url.Link<SubscriptorsController>(x => x.DetailedIndex(accountId), Rel.SubscriptorCollection, "Subscription list (detailed)"),
                },
                Items = new[]
                {
                    new SubscriptorRepresentation() {
                        Id = SAMPLE_ID,
                        Links = new[] {
                            Url.Link<SubscriptorsController>(x => x.Item(accountId, SAMPLE_ID), Rel.Self | Rel.SubscriptorItem, "Subscriptor details")
                        },
                        FirstName = "Juan",
                        LastName = "Perez"
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}/subscriptors/detail")]
        public IActionResult DetailedIndex(int accountId)
        {
            return new ObjectResult(new SubscriptorCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriptorDetailCollection),
                    Url.Link<AccountsController>(x => x.Item(accountId), Rel.Parent | Rel.AccountCollection, "Account details"),
                    Url.Link<SubscriptorsController>(x => x.Index(accountId), Rel.SubscriptorCollection, "Subscription list (simple)"),
                },
                Items = new[]
                {
                    new SubscriptorRepresentation() {
                        Links = new[] {
                            Url.Link<SubscriptorsController>(x => x.Item(accountId, SAMPLE_ID), Rel.Self | Rel.SubscriptorItem, "Subscriptor details")
                        },
                        Id = SAMPLE_ID,
                        FirstName = "Juan",
                        LastName = "Perez",
                        Birday = DateTime.Parse("1980-01-01"),
                        Email = "juan@perez.com"
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}/subscriptors/{subscriptorId}")]
        public IActionResult Item(int accountId, int subscriptorId)
        {
            return new ObjectResult(new AccountRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriptorItem),
                    Url.Link<SubscriptorsController>(x => x.Index(accountId), Rel.Parent | Rel.SubscriptorCollection, "Subscriptors list"),
                },
                Id = subscriptorId,
                FirstName = "Andrés",
                LastName = "Moschini",
                Email = "private@andresmoschini.com",
                Birday = DateTime.Parse("1978-12-02")
            });
        }
    }
}
